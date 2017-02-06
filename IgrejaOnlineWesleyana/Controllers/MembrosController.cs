using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using IgrejaOnlineWesleyana.Extensions;
using IgrejaOnlineWesleyana.Models;
using IgrejaOnlineWesleyana.Repositories;
using IgrejaOnlineWesleyana.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IgrejaOnlineWesleyana.Controllers
{
    public class MembrosController : Controller
    {
        private IMWModel db = new IMWModel();
        private FichaRepository _fichaRepository = new FichaRepository();
        private readonly MapperConfiguration _config;
        private readonly IMapper _mapper;

        public MembrosController()
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Membro, FichaCadastralViewModel>();
                cfg.CreateMap<FichaCadastralViewModel, Membro>();
            });
            _mapper = _config.CreateMapper();
        }
        // GET: Membros
        public async Task<ActionResult> Index()
        {
            var membro = db.Membro.Include(m => m.Cidade).Include(m => m.Cidade1).Include(m => m.Congregacao).Include(m => m.Distrito).Include(m => m.Estado).Include(m => m.GrauInstrucao).Include(m => m.Igreja).Include(m => m.Regiao);
            return View(await membro.ToListAsync());
        }

        // GET: Membros/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membro membro = await db.Membro.FindAsync(id);
            if (membro == null)
            {
                return HttpNotFound();
            }
            return View(membro);
        }

        [HttpGet]
        public async Task<ActionResult> NovaFicha()
        {
            var fichaCadastralViewModel = new FichaCadastralViewModel
            {
                Congregacoes = await _fichaRepository.GetCongregacoes(),
                Distritos = await _fichaRepository.GetDistritos(),
                Igrejas = await _fichaRepository.GetIgrejas(),
                Regioes = await _fichaRepository.GetRegioes(),
                GrausInstrucao = await _fichaRepository.GetGrausInstrucao(),
                Estados = await _fichaRepository.GetEstados(),
                Cidades = await _fichaRepository.GetCidades(),
                Naturalidades = await _fichaRepository.GetCidades(),
                TiposConjugue = await _fichaRepository.GetTipoConjugue()
            };
            return View("NovaFicha", fichaCadastralViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> NovaFicha(FichaCadastralViewModel fichaCadastralViewModel)
        {
            /*O campo Nome é obrigatório.,O campo Telefone é obrigatório.
             * ,O campo IDTipo é obrigatório.,O campo Email é obrigatório.
             * ,O campo IDRegiao é obrigatório.,O campo IDDistrito é obrigatório.
             * ,O campo IDIgreja é obrigatório.,O campo IDCongregacao é obrigatório.
             * ,O campo IDEstado é obrigatório.,O campo IDGrauInstrucao é obrigatório.
             * ,O campo IDCidade é obrigatório.,O campo IDNaturalidade é obrigatório.
             * ,O campo IDTipoConjugue é obrigatório.
*/
            try
            {
                fichaCadastralViewModel.Foto = JsonConvert.DeserializeObject<byte[]>(fichaCadastralViewModel.FotoEscolhida);
                if (ModelState.IsValid)
                {

                    var membro = _mapper.Map<FichaCadastralViewModel, Membro>(fichaCadastralViewModel);
                    db.Membro.Add(membro);
                    db.SaveChanges();
                    db.Membro.Attach(membro);
                    /*if (fichaCadastralViewModel.ConjugueMembro.Nome != null)
                    {
                        fichaCadastralViewModel.ConjugueMembro.IDEsposa = membro.ID;
                        db.Conjugue.Add(fichaCadastralViewModel.ConjugueMembro);
                        db.SaveChanges();
                    }*/
                    /*foreach (var filho in fichaCadastralViewModel.Filhos)
                    {
                        filho.IDMae = membro.ID;
                        db.Filho.Add(filho);
                        db.SaveChanges();
                    }*/
                    return RedirectToAction("Index");
                }

                Debug.WriteLine(string.Join(",",
                    ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray()));

                fichaCadastralViewModel = new FichaCadastralViewModel
                {
                    Congregacoes = await _fichaRepository.GetCongregacoes(),
                    Distritos = await _fichaRepository.GetDistritos(),
                    Igrejas = await _fichaRepository.GetIgrejas(),
                    Regioes = await _fichaRepository.GetRegioes(),
                    GrausInstrucao = await _fichaRepository.GetGrausInstrucao(),
                    Estados = await _fichaRepository.GetEstados(),
                    Cidades = await _fichaRepository.GetCidades(),
                    Naturalidades = await _fichaRepository.GetCidades(),
                    TiposConjugue = await _fichaRepository.GetTipoConjugue()
                };
                return View("NovaFicha", fichaCadastralViewModel);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return View("NovaFicha", fichaCadastralViewModel);
        }

        // GET: Membros/Create
        public ActionResult Create()
        {
            ViewBag.IDNaturalidade = new SelectList(db.Cidade, "ID", "Cidade1");
            ViewBag.IDCidade = new SelectList(db.Cidade, "ID", "Cidade1");
            ViewBag.IDCongregacao = new SelectList(db.Congregacao, "ID", "Nome");
            ViewBag.IDDistrito = new SelectList(db.Distrito, "ID", "Nome");
            ViewBag.IDEstado = new SelectList(db.Estado, "ID", "UF");
            ViewBag.IDGrauInstrucao = new SelectList(db.GrauInstrucao, "ID", "TipoGrau");
            ViewBag.IDIgreja = new SelectList(db.Igreja, "ID", "Nome");
            ViewBag.IDRegiao = new SelectList(db.Regiao, "ID", "Nome");
            return View();
        }

        // POST: Membros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,IDRegiao,IDDistrito,IDIgreja,IDCongregacao,Nome,Email,DataNascimento,Telefone,Celular,Nacionalidade,IDNaturalidade,Endereco,Complemento,Bairro,IDCidade,IDEstado,CEP,EstadoCivil,RG,OrgaoExpedidor,CPF,IDGrauInstrucao,Foto")] Membro membro)
        {
            if (ModelState.IsValid)
            {
                db.Membro.Add(membro);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDNaturalidade = new SelectList(db.Cidade, "ID", "Cidade1", membro.IDNaturalidade);
            ViewBag.IDCidade = new SelectList(db.Cidade, "ID", "Cidade1", membro.IDCidade);
            ViewBag.IDCongregacao = new SelectList(db.Congregacao, "ID", "Nome", membro.IDCongregacao);
            ViewBag.IDDistrito = new SelectList(db.Distrito, "ID", "Nome", membro.IDDistrito);
            ViewBag.IDEstado = new SelectList(db.Estado, "ID", "UF", membro.IDEstado);
            ViewBag.IDGrauInstrucao = new SelectList(db.GrauInstrucao, "ID", "TipoGrau", membro.IDGrauInstrucao);
            ViewBag.IDIgreja = new SelectList(db.Igreja, "ID", "Nome", membro.IDIgreja);
            ViewBag.IDRegiao = new SelectList(db.Regiao, "ID", "Nome", membro.IDRegiao);
            return View(membro);
        }

        // GET: Membros/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membro membro = await db.Membro.FindAsync(id);
            if (membro == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDNaturalidade = new SelectList(db.Cidade, "ID", "Cidade1", membro.IDNaturalidade);
            ViewBag.IDCidade = new SelectList(db.Cidade, "ID", "Cidade1", membro.IDCidade);
            ViewBag.IDCongregacao = new SelectList(db.Congregacao, "ID", "Nome", membro.IDCongregacao);
            ViewBag.IDDistrito = new SelectList(db.Distrito, "ID", "Nome", membro.IDDistrito);
            ViewBag.IDEstado = new SelectList(db.Estado, "ID", "UF", membro.IDEstado);
            ViewBag.IDGrauInstrucao = new SelectList(db.GrauInstrucao, "ID", "TipoGrau", membro.IDGrauInstrucao);
            ViewBag.IDIgreja = new SelectList(db.Igreja, "ID", "Nome", membro.IDIgreja);
            ViewBag.IDRegiao = new SelectList(db.Regiao, "ID", "Nome", membro.IDRegiao);
            return View(membro);
        }

        // POST: Membros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,IDRegiao,IDDistrito,IDIgreja,IDCongregacao,Nome,Email,DataNascimento,Telefone,Celular,Nacionalidade,IDNaturalidade,Endereco,Complemento,Bairro,IDCidade,IDEstado,CEP,EstadoCivil,RG,OrgaoExpedidor,CPF,IDGrauInstrucao,Foto")] Membro membro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(membro).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDNaturalidade = new SelectList(db.Cidade, "ID", "Cidade1", membro.IDNaturalidade);
            ViewBag.IDCidade = new SelectList(db.Cidade, "ID", "Cidade1", membro.IDCidade);
            ViewBag.IDCongregacao = new SelectList(db.Congregacao, "ID", "Nome", membro.IDCongregacao);
            ViewBag.IDDistrito = new SelectList(db.Distrito, "ID", "Nome", membro.IDDistrito);
            ViewBag.IDEstado = new SelectList(db.Estado, "ID", "UF", membro.IDEstado);
            ViewBag.IDGrauInstrucao = new SelectList(db.GrauInstrucao, "ID", "TipoGrau", membro.IDGrauInstrucao);
            ViewBag.IDIgreja = new SelectList(db.Igreja, "ID", "Nome", membro.IDIgreja);
            ViewBag.IDRegiao = new SelectList(db.Regiao, "ID", "Nome", membro.IDRegiao);
            return View(membro);
        }

        // GET: Membros/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membro membro = await db.Membro.FindAsync(id);
            if (membro == null)
            {
                return HttpNotFound();
            }
            return View(membro);
        }

        // POST: Membros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Membro membro = await db.Membro.FindAsync(id);
            db.Membro.Remove(membro);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
