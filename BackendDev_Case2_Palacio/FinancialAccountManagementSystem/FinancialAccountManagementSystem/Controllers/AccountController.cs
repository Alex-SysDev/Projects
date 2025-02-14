using AutoMapper;
using FinancialAccountManagementSystem.Data;
using FinancialAccountManagementSystem.Dto;
using FinancialAccountManagementSystem.Interfaces;
using FinancialAccountManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialAccountManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountController(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountDto>))]
        public IActionResult GetAccounts()
        {
            var accounts = _mapper.Map<List<AccountDto>>(_accountRepository.GetAccounts());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(accounts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(AccountDto))]
        [ProducesResponseType(400)]
        public IActionResult GetAccountById(int id)
        {
            if (!_accountRepository.AccountExists(id))
                return NotFound();

            var accountById = _mapper.Map<AccountDto>(_accountRepository.GetAccountById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(accountById);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAccount([FromBody] AccountWriteDto accountCreate)
        {
            if (accountCreate == null)
                return BadRequest(ModelState);

            var account = _accountRepository.GetAccounts()
                .Where(c => c.AccountHolder.Trim().ToUpper() == accountCreate.AccountHolder.TrimEnd())
                .FirstOrDefault();

            if (account != null)
            {
                ModelState.AddModelError("", "Account already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var accountMap = _mapper.Map<Account>(accountCreate);

            if (!_accountRepository.CreateAccount(accountMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAccount(int id, [FromBody] AccountDto updatedAccount)
        {
            if (updatedAccount == null)
                return BadRequest(ModelState);

            if (id != updatedAccount.Id)
                return BadRequest(ModelState);

            if (!_accountRepository.AccountExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var accountMap = _mapper.Map<Account>(updatedAccount);
            if (!_accountRepository.UpdateAccount(accountMap))
            {
                ModelState.AddModelError("", "Something went wrong updating account");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAccount(int id)
        {
            if (!_accountRepository.AccountExists(id))
                return NotFound();

            var accountToDelete = _accountRepository.GetAccountById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_accountRepository.DeleteAccount(accountToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting account");
            }

            return NoContent();
        }
    }
}
