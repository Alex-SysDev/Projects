using AutoMapper;
using FinancialAccountManagementSystem.Dto;
using FinancialAccountManagementSystem.Interfaces;
using FinancialAccountManagementSystem.Models;
using FinancialAccountManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FinancialAccountManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionDto>))]
        public IActionResult GetTransactions()
        {
            var transactions = _mapper.Map<List<TransactionDto>>(_transactionRepository.GetTransactions());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(transactions);
        }

        [HttpGet("id/{transactionId}")]
        [ProducesResponseType(200, Type = typeof(TransactionDto))]
        [ProducesResponseType(400)]
        public IActionResult GetTransactionById(int transactionId)
        {
            if (!_transactionRepository.TransactionExists(transactionId))
                return NotFound();

            var transactionById = _mapper.Map<TransactionDto>(_transactionRepository.GetTransactionById(transactionId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(transactionById);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTransaction([FromBody] TransactionCreateDto transactionCreate)
        {
            if (transactionCreate == null)
                return BadRequest(ModelState);

            var account = _accountRepository.GetAccounts().FirstOrDefault(a => a.Id == transactionCreate.AccountId);

            if (account == null)
            {
                ModelState.AddModelError("", "Account does not exist.");
                return StatusCode(404, ModelState);
            }

            if (transactionCreate.TransactionType == "Withdrawal" && account.Balance < transactionCreate.Amount)
            {
                ModelState.AddModelError("", "Insufficient funds.");
                return BadRequest(ModelState);
            }

            if (transactionCreate.TransactionType != "Withdrawal" && transactionCreate.TransactionType != "Deposit") 
            {
                ModelState.AddModelError("", "Invalid Transaction Type");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transactionMap = _mapper.Map<Transaction>(transactionCreate);
            transactionMap.TransactionDate = DateTime.Now;

            if (!_transactionRepository.CreateTransaction(transactionMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Transaction successfully created.");

        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTransaction(int id, [FromBody] TransactionDto updatedTransaction)
        {
            if (updatedTransaction == null)
                return BadRequest(ModelState);

            if (id != updatedTransaction.Id)
                return BadRequest(ModelState);

            if (!_transactionRepository.TransactionExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transactionMap = _mapper.Map<Transaction>(updatedTransaction);
            if (!_transactionRepository.UpdateTransaction(transactionMap))
            {
                ModelState.AddModelError("", "Something went wrong updating transaction");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTransaction(int id)
        {
            if (!_transactionRepository.TransactionExists(id))
                return NotFound();

            var transactionToDelete = _transactionRepository.GetTransactionById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_transactionRepository.DeleteTransaction(transactionToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting product");
            }

            return NoContent();
        }
    }
}
