using Microsoft.EntityFrameworkCore;
using TransportadorasApi.Data;
using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Model;

namespace TransportadorasApi.Repository
{
    public class DepositoRepository : IDepositoRepository
    {
        private readonly DataContext _context;
        public DepositoRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateDeposito(Deposito deposito)
        {
            var enderecoExistente = _context.Enderecos
            .FirstOrDefault(e =>
                e.Rua == deposito.Localizacao.Rua &&
                e.Bairro == deposito.Localizacao.Bairro &&
                e.Cidade == deposito.Localizacao.Cidade &&
                e.cep == deposito.Localizacao.cep &&
                e.Numero == deposito.Localizacao.Numero
            );

            if (enderecoExistente == null)
            {
                _context.Add(deposito);
            }
            else
            {
                int enderecoId = enderecoExistente.Id;


                deposito = new Deposito
                {
                    Localizacao = enderecoExistente
                };

                _context.Add(deposito);
            }
            return Save();
        }

        public bool DeleteDeposito(Deposito deposito)
        {
            _context.Remove(deposito);
            return Save();
        }

        public bool DepositoExists(int id)
        {
            return _context.Depositos.Any(d => d.Id == id);
        }

        public Deposito GetDeposito(int id)
        {
            return _context.Depositos.Include(d => d.Localizacao)
                .Where(d => d.Id == id).FirstOrDefault();
        }

        public Deposito GetDepositoByEndereco(int enderecoId)
        {
            return _context.Depositos.Include(d => d.Localizacao)
                    .Where(d => d.Localizacao.Id == enderecoId)
                    .FirstOrDefault();
        }


        public IQueryable<Endereco> getDepositoEndereco(int depositoId)
        {
            return _context.Depositos.Where(d => d.Id == depositoId)
               .Select(d => d.Localizacao);
        }

        public ICollection<Deposito> GetDepositos()
        {
            return _context.Depositos.Include(d => d.Localizacao).OrderBy(d => d.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDeposito(Deposito deposito)
        {
            var enderecoExistente = _context.Enderecos
                        .FirstOrDefault(e =>
                            e.Rua == deposito.Localizacao.Rua &&
                            e.Bairro == deposito.Localizacao.Bairro &&
                            e.Cidade == deposito.Localizacao.Cidade &&
                            e.cep == deposito.Localizacao.cep &&
                            e.Numero == deposito.Localizacao.Numero
                        );

            if (enderecoExistente == null)
            {
                return false;
            }
            else
            {
                int enderecoId = enderecoExistente.Id;


                deposito.Localizacao = enderecoExistente;

                _context.Update(deposito);
            }
            return Save();


        }
    }

}
