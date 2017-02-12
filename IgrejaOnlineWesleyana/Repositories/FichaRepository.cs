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
            ViewBag.IDCongregacao = new SelectList(db.Congregacoes, "ID", "NomeConjuge");
            ViewBag.IDDistrito = new SelectList(db.Distritos, "ID", "NomeConjuge");
            ViewBag.IDEstado = new SelectList(db.Estado, "ID", "UF");
            ViewBag.IDGrauInstrucao = new SelectList(db.GrauInstrucao, "ID", "TipoGrau");
            ViewBag.IDIgreja = new SelectList(db.Igrejas, "ID", "NomeConjuge");
            ViewBag.IDRegiao = new SelectList(db.Regiao, "ID", "NomeConjuge");*/
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
        public async Task<List<TipoConjuge>> GetTipoConjugue()
        {
            return await _dbImwModel.TipoConjuge.ToListAsync();
        }
        public async Task<Conjuge> GetConjugue(int idMembro)
        {
            return await _dbImwModel.Conjuge.FirstOrDefaultAsync(e => e.IDEsposa==idMembro);
        }
        public int GetMembroCPF(string CPF)
        {
            return  _dbImwModel.Membro.FirstOrDefault(e => e.CPF == CPF).ID;
        }

        public async Task<List<Filho>> GetFilhos(int idMembro)
        {
            return await _dbImwModel.Filho.Where(e => e.IDMae==idMembro).ToListAsync();
        }
        public void Dispose()
        {
            _dbImwModel.Dispose();
        }
    }
}