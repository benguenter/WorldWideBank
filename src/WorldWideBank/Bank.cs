using System.Linq;
using WorldWideBank.Actions;
using WorldWideBank.Domain;
using WorldWideBank.Factories;
using WorldWideBank.Requests;

namespace WorldWideBank
{
    public class Bank
    {
        public Bank(
            IAccountFactory accountFactory,
            ICurrencyActions currencyActions)
        {
            this._accountFactory = accountFactory;
            this._currencyActions = currencyActions;
        }

        /// <summary>
        /// Deposits money to the specified <see cref="Account"/> and converts the <see cref="CurrencyType"/> to <see cref="CurrencyType.CAD"/>.
        /// </summary>
        public Result<bool> Deposit(DepositRequest request)
        {
            var account = this._accountFactory.GetAccount(request.AccountNumber);
            // TODO: Check if account is found.

            var result = this._currencyActions.Add(account.Balance, request.Currency);
            if (!result.IsOk)
            {
                return Result.Error<bool>(result.Errors.ToArray());
            }

            account.Balance = result.Value;
            return Result.Ok(true);
        }

        /// <summary>
        /// Withdraws money from the specified <see cref="Account"/> and converts it to the destination <see cref="Currency"/>.
        /// </summary>
        public Result<bool> Withdraw(WithdrawRequest request)
        {
            var account = this._accountFactory.GetAccount(request.AccountNumber);
            // TODO: Check if account is found.
            // TODO: Check if user is authorized to withdraw from account.

            var result = this._currencyActions.Subtract(account.Balance,request.Currency);
            if (result.IsError)
            {
                return Result.Error<bool>(result.Errors.ToArray());
            }

            if (result.Value.Amount < 0)
            {
                return Result.Error<bool>("Cannot withdraw more than the balance of the account.");
            }

            account.Balance = result.Value;
            return Result.Ok(true);
        }

        /// <summary>
        /// Moves money from the specified input <see cref="Account"/> to the specified destination <see cref="Account"/>.
        /// </summary>
        public Result<bool> Transfer(TransferRequest request)
        {
            var withdrawRequest = new WithdrawRequest
            {
                AccountNumber = request.SourceAccountNumber,
                Currency = request.Currency
            };
            var withdrawResult = this.Withdraw(withdrawRequest);
            if (withdrawResult.IsError)
            {
                return withdrawResult;
            }

            var depositRequest = new DepositRequest
            {
                AccountNumber = request.DestinationAccountNumber,
                Currency = request.Currency
            };
            var depositResult = this.Deposit(depositRequest);
            // TODO: Unintended side effect here if the Deposit action fails, the withdraw request has already completed.
            // TODO: Ideally, this would all be wrapped in some sort of transaction that can be rolled back from persistence.
            return depositResult.IsError
                ? depositResult
                : Result.Ok(true);
        }

        private readonly IAccountFactory _accountFactory;
        private readonly ICurrencyActions _currencyActions;
    }
}