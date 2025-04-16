using AutoMapper;
using FinLedgerSystem.Data;
using FinLedgerSystem.Models;
using FinLedgerSystem.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FinLedgerSystem.Controllers
{
    [Route("api/ledger")]
    [ApiController]
    public class LedgerPostingController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        public LedgerPostingController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet("accounts")]
        public ResponseDto GetLedgerAccounts()
        {
            try
            {
                IEnumerable<LedgerAccount> ledgerAccounts = _db.LedgerAccounts.ToList();
                _responseDto.Result = _mapper.Map<List<LedgerAccountDTO>>(ledgerAccounts);
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }


            return _responseDto;
        }


        [HttpGet("accounts/{id}")]
        public ResponseDto GetLedgerAccounts(int id)
        {
            try
            {
                LedgerAccount ledgerAccount = _db.LedgerAccounts.FirstOrDefault(x => x.Id == id);
                _responseDto.Result = _mapper.Map<LedgerAccountDTO>(ledgerAccount);
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }


            return _responseDto;
        }



        [HttpPost("accounts")]
        public ResponseDto PostLedgerAccounts([FromBody] LedgerAccountDTO ledgerAccountDto)
        {
            try
            {
                LedgerAccount ledgerAccount = _mapper.Map<LedgerAccount>(ledgerAccountDto);
                ledgerAccount.IsActive = true;
                _db.LedgerAccounts.Add(ledgerAccount);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<LedgerAccountDTO>(ledgerAccount);
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }



        [HttpPut("accounts")]
        public ResponseDto UpdateLedgerAccounts([FromBody] LedgerAccountDTO ledgerAccountDto)
        {
            try
            {
                LedgerAccount ledgerAccount = _mapper.Map<LedgerAccount>(ledgerAccountDto);
                _db.LedgerAccounts.Update(ledgerAccount);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<LedgerAccountDTO>(ledgerAccount);
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }

            return _responseDto;
        }



        [HttpDelete("accounts/{id}")]
        public ResponseDto RemoveLedgerAccounts(int id)
        {
            try
            {
                LedgerAccount ledgerAccount = _db.LedgerAccounts.FirstOrDefault(x => x.Id == id);
                ledgerAccount.IsActive = false;
                _responseDto.Message = "Deleted";
                _db.LedgerAccounts.Update(ledgerAccount);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<LedgerAccountDTO>(ledgerAccount);
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }


        [HttpPost("purchase/invoice")]
        public ResponseDto PostPurchaseInvoice([FromBody] PurchaseInvoiceTransactionDTO obj)
        {
            try
            {
                PurchaseInvoiceTransaction invoiceTransaction = _mapper.Map<PurchaseInvoiceTransaction>(obj);
                _db.PurchaseInvoiceTransactions.Add(invoiceTransaction);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<PurchaseInvoiceTransactionDTO>(invoiceTransaction);


            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }



        [HttpPost("posting")]
        public ResponseDto ProcessLedgersForPurcahseInvoice([FromBody] LedgerTransactionDTO obj)
        {
            try
            {
                IEnumerable<PurchaseInvoiceTransaction> invoiceTransactions = new List<PurchaseInvoiceTransaction>();

                if (obj.InvoiceId != 0)
                {
                    invoiceTransactions = _db.PurchaseInvoiceTransactions.Where(x => x.Id == obj.InvoiceId).ToList();
                }
                else
                {
                    invoiceTransactions = _db.PurchaseInvoiceTransactions.Where(x => x.IsPosted == false && x.IsValid == true).ToList();
                }

                foreach (var transaction in invoiceTransactions)
                {
                    // Dr Receivable Account: 120010(Asset)
                    AddLedgerEntry(ledgerId: 1, dr: transaction.Amount, cr: 0, transaction, transaction.TransactionCode);

                    //Dr Tax Receivable Account: 260100(Asset as it can be claimed back)
                    if (transaction.TaxAmount > 0)
                    {
                        AddLedgerEntry(ledgerId: 2, dr: transaction.TaxAmount, cr: 0, transaction, transaction.TransactionCode);
                    }

                    //Cr Suppliers Account: 210600(Liability)
                    AddLedgerEntry(ledgerId: 3, dr: 0, cr: transaction.TotalAmount, transaction, transaction.TransactionCode);
                }


            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }


        private void AddLedgerEntry(int ledgerId, float dr, float cr, PurchaseInvoiceTransaction invoice, string code)
        {
            LedgerTransaction entry = new()
            {
                LedgerId = ledgerId,
                DrAmount = dr,
                CrAmount = cr,
                TransactionType = "PURCHASE_INVOICE",
                TransactionCode = code,
                TransactionReference = invoice.InvoiceNumber,
                SourceId = invoice.Id,
                CreatedDate = DateTime.UtcNow
            };
            _db.LedgerTransactions.Add(entry);
            _db.SaveChanges();
        }


    }
}
