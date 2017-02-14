using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using IgrejaOnlineWesleyana.Extensions;
using IgrejaOnlineWesleyana.Models;
using IgrejaOnlineWesleyana.ViewModels;
using Newtonsoft.Json;

namespace IgrejaOnlineWesleyana.Controllers
{
    public class MembrosController : Controller
    {
        private readonly IMWModel _db = new IMWModel();
        private readonly IMapper _mapper;

        public MembrosController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Membro, FichaCadastralViewModel>();
                cfg.CreateMap<Conjuge, FichaCadastralViewModel>();
                cfg.CreateMap<FichaCadastralViewModel, Membro>();
                cfg.CreateMap<FichaCadastralViewModel, Conjuge>();
            });
            _mapper = config.CreateMapper();
        }

        // GET: Membros
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var membro =
                _db.Membro.Include(m => m.Cidade)
                    .Include(m => m.Cidade1)
                    .Include(m => m.Congregacao)
                    .Include(m => m.Distrito)
                    .Include(m => m.Estado)
                    .Include(m => m.GrauInstrucao)
                    .Include(m => m.Igreja)
                    .Include(m => m.Regiao);
            return View(await membro.OrderBy(e => e.Nome).ToListAsync());
        }

        [NoDirectAccess]
        [Authorize]
        // GET: Membros/Details/5
        public async Task<ActionResult> DetalhesFicha(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var membro = await _db.Membro.FindAsync(id);
            if (membro == null)
            {
                return HttpNotFound();
            }
            return View(membro);
        }

        [HttpGet]
        [Authorize]

        public async Task<ActionResult> NovaFicha()
        {
            var fichaCadastralViewModel = new FichaCadastralViewModel
            {
                Congregacoes = await _db.Congregacao.ToListAsync(),
                Distritos = await _db.Distrito.ToListAsync(),
                Igrejas = await _db.Igreja.ToListAsync(),
                Regioes = await _db.Regiao.ToListAsync(),
                GrausInstrucao = await _db.GrauInstrucao.ToListAsync(),
                Estados = await _db.Estado.ToListAsync(),
                Cidades = await _db.Cidade.ToListAsync(),
                Naturalidades = await _db.Cidade.ToListAsync(),
                TiposConjuge = await _db.TipoConjuge.ToListAsync()
            };
            return View("NovaFicha", fichaCadastralViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NovaFicha(FichaCadastralViewModel fichaCadastralViewModel)
        {
            var jsonResult = new JsonResult();
            try
            {
                var validator = new CPFValidatorViewModel();
                var result = validator.Validate(fichaCadastralViewModel);
                var firstOrDefault = result.Errors.FirstOrDefault();
                if (ModelState.IsValid && result.IsValid)
                {
                    fichaCadastralViewModel.Foto =
                        JsonConvert.DeserializeObject<byte[]>(fichaCadastralViewModel.FotoEscolhida);
                    var membro = _mapper.Map<FichaCadastralViewModel, Membro>(fichaCadastralViewModel);
                    _db.Membro.Add(membro);
                    _db.SaveChanges();
                    if (fichaCadastralViewModel.NomeConjuge != null)
                    {
                        fichaCadastralViewModel.IDEsposa = membro.ID;
                        var conjuge = _mapper.Map<FichaCadastralViewModel, Conjuge>(fichaCadastralViewModel);
                        _db.Conjuge.Add(conjuge);
                        _db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }

                var errorModel =
                    from x in ModelState.Keys
                    where ModelState[x].Errors.Count > 0
                    select new
                    {
                        key = x,
                        errors = ModelState[x].Errors.
                            Select(y => y.ErrorMessage).
                            ToArray()
                    };

                if (firstOrDefault != null)
                {
                    var error = "";
                    var x = errorModel.ToList();
                    foreach (var item in errorModel)
                    {
                        var a = 0;
                        error = error + " " + item.errors[a];
                        a++;
                    }
                    jsonResult = new JsonResult()
                    {
                        Data = firstOrDefault.ErrorMessage
                    };
                }
                else
                    jsonResult = new JsonResult()
                    {
                        Data = errorModel
                    };

                jsonResult.ContentEncoding = Encoding.UTF8;
                HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;

                fichaCadastralViewModel = new FichaCadastralViewModel
                {
                    Congregacoes = await _db.Congregacao.ToListAsync(),
                    Distritos = await _db.Distrito.ToListAsync(),
                    Igrejas = await _db.Igreja.ToListAsync(),
                    Regioes = await _db.Regiao.ToListAsync(),
                    GrausInstrucao = await _db.GrauInstrucao.ToListAsync(),
                    Estados = await _db.Estado.ToListAsync(),
                    Cidades = await _db.Cidade.ToListAsync(),
                    Naturalidades = await _db.Cidade.ToListAsync(),
                    TiposConjuge = await _db.TipoConjuge.ToListAsync()
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);

            }
            return Json(new {data = jsonResult}, JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        [HttpGet]
        public async Task<ActionResult> BuscarIDPorCPF(string cpf)
        {
            if (cpf == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var membro = await _db.Membro.FirstOrDefaultAsync(e => e.CPF == cpf);
            if (membro == null)
            {
                return HttpNotFound();
            }
            return Json(new {data = membro.ID}, JsonRequestBehavior.AllowGet);

        }

        [NoDirectAccess]
        [HttpGet]
        public async Task<ActionResult> AlterarFicha(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var membro = await _db.Membro.FindAsync(id);
            if (membro == null)
            {
                return HttpNotFound();
            }
            var conjuge = await _db.Conjuge.FirstOrDefaultAsync(e => e.IDEsposa == membro.ID);
            var filhos = await _db.Filho.Where(e => e.IDMae == membro.ID).ToListAsync();
            var fichaCadastralViewModel = _mapper.Map<Membro, FichaCadastralViewModel>(membro);
            fichaCadastralViewModel.Congregacoes = await _db.Congregacao.ToListAsync();
            fichaCadastralViewModel.Distritos = await _db.Distrito.ToListAsync();
            fichaCadastralViewModel.Igrejas = await _db.Igreja.ToListAsync();
            fichaCadastralViewModel.Regioes = await _db.Regiao.ToListAsync();
            fichaCadastralViewModel.GrausInstrucao = await _db.GrauInstrucao.ToListAsync();
            fichaCadastralViewModel.Estados = await _db.Estado.ToListAsync();
            fichaCadastralViewModel.Cidades = await _db.Cidade.ToListAsync();
            fichaCadastralViewModel.Naturalidades = await _db.Cidade.ToListAsync();
            fichaCadastralViewModel.TiposConjuge = await _db.TipoConjuge.ToListAsync();
            if (conjuge != null)
            {
                fichaCadastralViewModel.IDConjuge = conjuge.IDConjuge;
                fichaCadastralViewModel.DataNascimentoConjuge = conjuge.DataNascimentoConjuge;
                fichaCadastralViewModel.EmailConjuge = conjuge.EmailConjuge;
                fichaCadastralViewModel.NomeConjuge = conjuge.NomeConjuge;
                fichaCadastralViewModel.TelefoneConjuge = conjuge.TelefoneConjuge;
                fichaCadastralViewModel.IDTipoConjuge = conjuge.IDTipoConjuge;
                fichaCadastralViewModel.IDEsposa = conjuge.IDEsposa;

            }
            if (filhos.Count > 0)
                fichaCadastralViewModel.Filhos = filhos;
            return View("AlterarFicha", fichaCadastralViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AlterarFicha(FichaCadastralViewModel fichaCadastralViewModel)
        {
            var jsonResult = new JsonResult();
            try
            {
                fichaCadastralViewModel.Foto =
                    JsonConvert.DeserializeObject<byte[]>(fichaCadastralViewModel.FotoEscolhida);
                if (ModelState.IsValid)
                {
                    var membro = _mapper.Map<FichaCadastralViewModel, Membro>(fichaCadastralViewModel);
                    _db.Entry(membro).State = EntityState.Modified;
                    _db.SaveChanges();
                    if (fichaCadastralViewModel.NomeConjuge == null) return RedirectToAction("Index");
                    fichaCadastralViewModel.IDEsposa = membro.ID;
                    var conjuge = _mapper.Map<FichaCadastralViewModel, Conjuge>(fichaCadastralViewModel);
                    fichaCadastralViewModel.IDEsposa = membro.ID;
                    _db.Entry(conjuge).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }

                var errorModel =
                    from x in ModelState.Keys
                    where ModelState[x].Errors.Count > 0
                    select new
                    {
                        key = x,
                        errors = ModelState[x].Errors.
                            Select(y => y.ErrorMessage).
                            ToArray()
                    };

                jsonResult = new JsonResult
                {
                    Data = errorModel,
                    ContentEncoding = Encoding.UTF8
                };

                HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;

                fichaCadastralViewModel = new FichaCadastralViewModel
                {
                    Congregacoes = await _db.Congregacao.ToListAsync(),
                    Distritos = await _db.Distrito.ToListAsync(),
                    Igrejas = await _db.Igreja.ToListAsync(),
                    Regioes = await _db.Regiao.ToListAsync(),
                    GrausInstrucao = await _db.GrauInstrucao.ToListAsync(),
                    Estados = await _db.Estado.ToListAsync(),
                    Cidades = await _db.Cidade.ToListAsync(),
                    Naturalidades = await _db.Cidade.ToListAsync(),
                    TiposConjuge = await _db.TipoConjuge.ToListAsync()
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return Json(new {data = jsonResult}, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> ExcluirFicha(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membro membro = await _db.Membro.FindAsync(id);
            if (membro == null)
            {
                return HttpNotFound();
            }
            return View(membro);
        }

        // POST: Membros/Delete/5
        [HttpPost, ActionName("ExcluirFicha")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> ExcluirFichaConfirmed(int id)
        {
            try
            {
                Membro membro = await _db.Membro.FindAsync(id);
                foreach (var filho in membro.Filhos.ToList())
                {
                    _db.Filho.Remove(filho);
                }
                var conjuge = await _db.Conjuge.FirstOrDefaultAsync(e => e.IDEsposa == membro.ID);
                if (conjuge != null)
                    _db.Conjuge.Remove(conjuge);
                _db.Membro.Remove(membro);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
