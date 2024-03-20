using NerdStore.Core;
using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Domain
{
    public class Pedido : Entity, IAggregateRoot
    {
        public int Codigo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUtilizado { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }

        //Lista interna para evitar manipulação
        private readonly List<PedidoItem> _pedidoItems;
        //Lista externa  
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        //EF Rel.
        public Voucher Voucher { get; private set; }

    }
}
