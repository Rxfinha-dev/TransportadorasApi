using Microsoft.EntityFrameworkCore;
using TransportadorasApi.Data;
using TransportadorasApi.Interface;
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

            if ( enderecoExistente == null)
            {
                _context.Add(deposito);
            }else
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
            return _context.Depositos.Any(d=>d.Id == id);
        }

        public Deposito GetDeposito(int id)
        {
            return _context.Depositos.Include(d=>d.Localizacao)
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
            return _context.Depositos.Include(d=>d.Localizacao).OrderBy(d=>d.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDeposito(int depositoId, Deposito depositoAtualizado)
        {
            // 1️⃣ Buscar o depósito existente
            var depositoDb = _context.Depositos
                .Include(d => d.Localizacao)
                .FirstOrDefault(d => d.Id == depositoId);

            if (depositoDb == null)
                return false;

            // 2️⃣ Verificar se o endereço enviado já existe no banco
            var enderecoExistente = _context.Enderecos.FirstOrDefault(e =>
                e.Rua == depositoAtualizado.Localizacao.Rua &&
                e.Bairro == depositoAtualizado.Localizacao.Bairro &&
                e.Cidade == depositoAtualizado.Localizacao.Cidade &&
                e.cep == depositoAtualizado.Localizacao.cep &&
                e.Numero == depositoAtualizado.Localizacao.Numero
            );

            int enderecoId;

            if (enderecoExistente != null)
            {
                // 3️⃣ Se já existe → apenas usa a FK existente
                enderecoId = enderecoExistente.Id;
            }
            else
            {
                // 4️⃣ Se não existe → criar um novo endereço
                var novoEndereco = new Endereco
                {
                    Rua = depositoAtualizado.Localizacao.Rua,
                    Bairro = depositoAtualizado.Localizacao.Bairro,
                    Cidade = depositoAtualizado.Localizacao.Cidade,
                    cep = depositoAtualizado.Localizacao.cep,
                    Numero = depositoAtualizado.Localizacao.Numero
                };

                _context.Enderecos.Add(novoEndereco);
                _context.SaveChanges();

                enderecoId = novoEndereco.Id;
            }

            // 5️⃣ Atualizar apenas o LocalizacaoId do depósito
            depositoDb.Localizacao.Id = enderecoId;

            _context.Update(depositoDb);

            return Save();
        }

    }

}
