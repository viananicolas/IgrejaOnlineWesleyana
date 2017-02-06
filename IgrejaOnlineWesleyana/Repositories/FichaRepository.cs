using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using IgrejaOnlineWesleyana.Models;

namespace IgrejaOnlineWesleyana.Repositories
{

    /*            ViewBag.IDNaturalidade = new SelectList(db.Cidade, "ID", "Cidade1");
            ViewBag.IDCidade = new SelectList(db.Cidade, "ID", "Cidade1");
            ViewBag.IDCongregacao = new SelectList(db.Congregacoes, "ID", "Nome");
            ViewBag.IDDistrito = new SelectList(db.Distritos, "ID", "Nome");
            ViewBag.IDEstado = new SelectList(db.Estado, "ID", "UF");
            ViewBag.IDGrauInstrucao = new SelectList(db.GrauInstrucao, "ID", "TipoGrau");
            ViewBag.IDIgreja = new SelectList(db.Igrejas, "ID", "Nome");
            ViewBag.IDRegiao = new SelectList(db.Regiao, "ID", "Nome");*/
    public class FichaRepository:IDisposable
    {
        private readonly IMWModel _dbImwModel = new IMWModel();
        public async Task<List<Regiao>> GetRegioes()
        {
            return await _dbImwModel.Regiao.ToListAsync();
        }
        public async Task<List<Igreja>> GetIgrejas()
        {
            return await _dbImwModel.Igreja.ToListAsync();
        }
        public async Task<List<Estado>> GetEstados()
        {
            return await _dbImwModel.Estado.ToListAsync();
        }
        public async Task<List<Distrito>> GetDistritos()
        {
            return await _dbImwModel.Distrito.ToListAsync();
        }
        public async Task<List<Congregacao>> GetCongregacoes()
        {
            return await _dbImwModel.Congregacao.ToListAsync();
        }
        public async Task<List<Cidade>> GetCidades()
        {
            return await _dbImwModel.Cidade.ToListAsync();
        }
        public async Task<List<GrauInstrucao>> GetGrausInstrucao()
        {
            return await _dbImwModel.GrauInstrucao.ToListAsync();
        }
        public async Task<List<TipoConjugue>> GetTipoConjugue()
        {
            return await _dbImwModel.TipoConjugue.ToListAsync();
        }
        public void Dispose()
        {
            _dbImwModel.Dispose();
        }
    }
}