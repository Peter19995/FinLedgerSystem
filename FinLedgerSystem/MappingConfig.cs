using AutoMapper;
using FinLedgerSystem.Models;
using FinLedgerSystem.Models.DTO;

namespace FinLedgerSystem
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {

                mc.CreateMap<LedgerAccount, LedgerAccountDTO>().ReverseMap();
                mc.CreateMap<PurchaseInvoiceTransaction, PurchaseInvoiceTransactionDTO>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
