using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using Yadebs.Db;
using Yadebs.Models.Dto;

namespace Yadebs.Bll.Services
{
    public class AccountingService : IAccountingService
    {
        AccountingContext context;

        public AccountingService(AccountingContext context)
        {
            this.context = context;
        }

        public async Task<AccountDto> AddAccountAsync(AccountDto accountDto)
        {
            var account = new Account
            {
                BookId = accountDto.BookId,
                Name = accountDto.Name,
                Number = accountDto.Number
            };
            this.context.Accounts.Add(account);
            await this.context.SaveChangesAsync();
            return await GetAccountAsync(account.Id);
        }

        public async Task<AccountDto> GetAccountAsync(int id)
        {
            var account = await this.context.Accounts.SingleAsync(a => a.Id == id);
            return new AccountDto
            {
                Id = account.Id,
                Name = account.Name,
                Number = account.Number,
                BookId = account.BookId
            };
        }

        public async Task<IEnumerable<AccountDto>> GetAccountsAsync()

        {
            return await this.context.Accounts.Select(a => new AccountDto
            {
                Id = a.Id,
                Name = a.Name,
                Number = a.Number,
                BookId= a.BookId,
                ParentId = a.ParentId
            }).ToListAsync();
        }

        private AccountTreeNode GetNodeWithChildren(AccountDto node , IEnumerable<AccountDto> nodes)
        {
            var children = nodes.Where(a => a.ParentId == node.Id).ToList();
            List<AccountTreeNode>? childrenAccountTreeNodes = null;
            if (children.Any())
            {
                childrenAccountTreeNodes = new List<AccountTreeNode>();
                foreach (var child in children)
                {
                    childrenAccountTreeNodes.Add(GetNodeWithChildren(child,nodes));
                }
            }
            var accountTreeNode = new AccountTreeNode
            {
                Id=node.Id,
                Name=node.Name,
                ParentId = node.ParentId,
                Number = node.Number,
                Children = childrenAccountTreeNodes?.ToArray()
            };
            return accountTreeNode;
        }

        public async Task<IEnumerable<AccountTreeNode>> GetAccountTreeAsync()
        {
            var nodes = await GetAccountsAsync();
            List<AccountTreeNode> tree = new List<AccountTreeNode>();

            foreach (var rootNode in nodes.Where(a => a.ParentId == null))
            {
                tree.Add(GetNodeWithChildren(rootNode, nodes));
            }

            return tree;
        }


        public async Task DeleteAccountAsync(int id)
        {
            var account = await this.context.Accounts.SingleAsync(a => a.Id == id);
            this.context.Accounts.Remove(account);
            await this.context.SaveChangesAsync();
        }
    }
}