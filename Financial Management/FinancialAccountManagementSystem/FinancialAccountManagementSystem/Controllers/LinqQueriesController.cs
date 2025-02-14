using AutoMapper;
using FinancialAccountManagementSystem.Dto;
using FinancialAccountManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancialAccountManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinqQueriesController : Controller
    {
        private readonly ILinqQueriesRepository _linqRepository;
        private readonly IMapper _mapper;

        public LinqQueriesController(ILinqQueriesRepository linqRepository, IMapper mapper)
        {
            _linqRepository = linqRepository;
            _mapper = mapper;
        }

        [HttpGet("transactions/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetTransactionsByAccount(int id)
        {
            if (!_linqRepository.AccountExists(id))
                return NotFound();

            var transactionsByAccount = _mapper.Map<List<TransactionDto>>(_linqRepository.GetTransactionsByAccount(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(transactionsByAccount);
        }

        [HttpGet("total-balance")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountsTotalDto>))]
        public IActionResult GetTotalBalance()
        {
            var totalBalanceOfAccounts = _mapper.Map<AccountsTotalDto>(_linqRepository.GetTotalBalance());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(totalBalanceOfAccounts);
        }
        [HttpGet("accounts-below-balance")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountDto>))]
        public IActionResult GetBalanceBelow(decimal treshold)
        {
            if (treshold <= 0)
            {
                return BadRequest("Threshold must be a positive number.");
            }

            var balanceBelow = _mapper.Map<List<AccountDto>>(_linqRepository.GetBalanceBelow(treshold));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(balanceBelow);
        }
        [HttpGet("top-five-accounts")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountDto>))]
        public IActionResult GetHighestBalance()
        {
            var topFiveAccounts = _mapper.Map<List<AccountDto>>(_linqRepository.GetHighestBalance());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(topFiveAccounts);
        }
    }
}
