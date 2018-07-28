//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Globalization;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Reflection;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace Himalayan_Angular.Data.Stores
//{
//    public class UserStore<TUser> : IUserStore<TUser>,
//             //IUserLoginStore<TUser>,
//             IUserClaimStore<TUser>,
//             IUserRoleStore<TUser, string>,
//             IUserPasswordStore<TUser>,
//             IUserSecurityStampStore<TUser>,
//             IQueryableUserStore<TUser>,
//             IUserTwoFactorStore<TUser, string>,
//             IUserLockoutStore<TUser, string>,
//             IUserEmailStore<TUser>,
//             IUserPhoneNumberStore<TUser>,
//             // ReSharper disable once RedundantExtendsListEntry
//             IDisposable where TUser : User
//    {
//        private readonly IDbSet<Login> _logins;
//        private readonly EntityStore<RoleEntity> _roleStore;
//        private readonly IDbSet<Claim> _userClaims;
//        private readonly IDbSet<Role> _userRoles;
//        private bool _disposed;
//        private EntityStore<TUser> _userStore;

//        /// <summary>
//        /// If true will call dispose on the DbContext during Dispose
//        /// 
//        /// </summary>
//        public bool DisposeContext { get; set; }
//        /// <summary>
//        /// Context for the store
//        /// 
//        /// </summary>
//        public DbContext Context { get; private set; }

//        public UserStore() : this(new IdentityDbContext())
//        {
//            DisposeContext = true;
//        }

//        public UserStore(DbContext context)
//        {
//            if (context == null)
//                throw new ArgumentNullException(nameof(context));
//            Context = context;
//            _userStore = new EntityStore<TUser>(context);
//            _roleStore = new EntityStore<RoleEntity>(context);
//            _logins = Context.Set<Login>();
//            _userClaims = Context.Set<Claim>();
//            _userRoles = Context.Set<Role>();
//        }

//        public IQueryable<TUser> Users => _userStore.EntitySet;

//        public Task AddClaimAsync(TUser user, System.Security.Claims.Claim claim)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            if (claim == null)
//                throw new ArgumentNullException(nameof(claim));
//            IDbSet<Claim> dbSet = _userClaims;
//            Claim instance = Activator.CreateInstance<Claim>();
//            instance.UserId = user.Id;
//            instance.ClaimType = claim.Type;
//            instance.ClaimValue = claim.Value;
//            Claim entity = instance;
//            dbSet.Add(entity);
//            return Task.FromResult(0);
//        }

//        public Task AddLoginAsync(TUser user, UserLoginInfo login)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            if (login == null)
//                throw new ArgumentNullException(nameof(login));
//            IDbSet<Login> dbSet = _logins;
//            Login instance = Activator.CreateInstance<Login>();
//            instance.UserId = user.Id;
//            instance.ProviderKey = login.ProviderKey;
//            instance.LoginProvider = login.LoginProvider;
//            Login entity = instance;
//            dbSet.Add(entity);
//            return Task.FromResult(0);
//        }

//        public async Task CreateAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            _userStore.Create(user);
//            await SaveChanges();
//        }

//        private async Task SaveChanges()
//        {
//            ThrowIfDisposed();
//            await Context.SaveChangesAsync();
//        }

//        public async Task DeleteAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            _userStore.Delete(user);
//            await SaveChanges();
//        }

//        public async Task<TUser> FindAsync(UserLoginInfo login)
//        {
//            ThrowIfDisposed();
//            if (login == null)
//                throw new ArgumentNullException(nameof(login));
//            string provider = login.LoginProvider;
//            string key = login.ProviderKey;
//            Login userLogin = await _logins.FirstOrDefaultAsync(l => l.LoginProvider == provider && l.ProviderKey == key);
//            TUser user;
//            if (userLogin != null)
//            {
//                var userId = userLogin.UserId;
//                user = await GetUserAggregateAsync((u => u.Id.Equals(userId)));
//            }
//            else
//                user = default(TUser);
//            return user;
//        }

//        private async Task<TUser> GetUserAggregateAsync(Expression<Func<TUser, bool>> filter)
//        {
//            string id;
//            TUser user;
//            if (FindByIdFilterParser.TryMatchAndGetId(filter, out id))
//                user = await _userStore.GetByIdAsync(id);
//            else
//                user = await Users.FirstOrDefaultAsync(filter);
//            if (user != null)
//            {
//                await EnsureClaimsLoaded(user);
//                await EnsureLoginsLoaded(user);
//                await EnsureRolesLoaded(user);
//            }
//            return user;
//        }

//        public Task<TUser> FindByEmailAsync(string email)
//        {
//            ThrowIfDisposed();
//            return GetUserAggregateAsync((u => u.Email.ToUpper() == email.ToUpper()));
//        }

//        public Task<TUser> FindByIdAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return GetUserAggregateAsync((u => u.Id.Equals(userId)));
//        }

//        public Task<TUser> FindByNameAsync(string userName)
//        {
//            ThrowIfDisposed();
//            return GetUserAggregateAsync((u => u.UserName.ToUpper() == userName.ToUpper()));
//        }

//        public Task<int> GetAccessFailedCountAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            return Task.FromResult(user.AccessFailedCount);
//        }

//        public async Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            await EnsureClaimsLoaded(user);
//            return (IList<System.Security.Claims.Claim>)user.Claims.Select(c => new System.Security.Claims.Claim(c.ClaimType, c.ClaimValue)).ToList();

//        }

//        public Task<string> GetEmailAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            return Task.FromResult(user.Email);
//        }

//        public Task<bool> GetEmailConfirmedAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            return Task.FromResult(user.EmailConfirmed);
//        }

//        public Task<bool> GetLockoutEnabledAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            return Task.FromResult(user.LockoutEnabled);
//        }

//        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            return Task.FromResult(user.LockoutEndDateUtc.HasValue ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc)) : new DateTimeOffset());
//        }

//        public Task<string> GetPasswordHashAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            return Task.FromResult(user.PasswordHash);
//        }

//        public Task<string> GetPhoneNumberAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            return Task.FromResult(user.PhoneNumber);
//        }

//        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            return Task.FromResult(user.PhoneNumberConfirmed);
//        }

//        public Task<string> GetSecurityStampAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            return Task.FromResult(user.SecurityStamp);
//        }

//        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            return Task.FromResult(user.TwoFactorEnabled);
//        }

//        public Task<bool> HasPasswordAsync(TUser user)
//        {
//            return Task.FromResult(user.PasswordHash != null);
//        }

//        public Task<int> IncrementAccessFailedCountAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            ++user.AccessFailedCount;
//            return Task.FromResult(user.AccessFailedCount);
//        }

//        public async Task RemoveClaimAsync(TUser user, System.Security.Claims.Claim claim)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            if (claim == null)
//                throw new ArgumentNullException(nameof(claim));
//            string claimValue = claim.Value;
//            string claimType = claim.Type;
//            IEnumerable<Claim> claims;
//            if (AreClaimsLoaded(user))
//            {
//                claims = user.Claims.Where(uc =>
//                {
//                    if (uc.ClaimValue == claimValue)
//                        return uc.ClaimType == claimType;
//                    return false;
//                }).ToList();
//            }
//            else
//            {
//                var userId = user.Id;
//                claims = await _userClaims.Where(uc => uc.ClaimValue == claimValue && uc.ClaimType == claimType && uc.UserId.Equals(userId)).ToListAsync();
//            }
//            foreach (Claim entity in claims)
//                _userClaims.Remove(entity);
//        }

//        public async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            if (login == null)
//                throw new ArgumentNullException(nameof(login));
//            string provider = login.LoginProvider;
//            string key = login.ProviderKey;
//            Login entry;
//            if (AreLoginsLoaded(user))
//            {
//                entry = user.Logins.SingleOrDefault(ul =>
//                {
//                    if (ul.LoginProvider == provider)
//                        return ul.ProviderKey == key;
//                    return false;
//                });
//            }
//            else
//            {
//                var userId = user.Id;
//                entry = await _logins.SingleOrDefaultAsync(ul => ul.LoginProvider == provider && ul.ProviderKey == key && ul.UserId.Equals(userId));
//            }
//            if (entry != null)
//                _logins.Remove(entry);
//        }

//        public Task ResetAccessFailedCountAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            user.AccessFailedCount = 0;
//            return Task.FromResult(0);
//        }

//        public Task SetEmailAsync(TUser user, string email)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            user.Email = email;
//            return Task.FromResult(0);
//        }

//        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            user.EmailConfirmed = confirmed;
//            return Task.FromResult(0);
//        }

//        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            user.LockoutEnabled = enabled;
//            return Task.FromResult(0);
//        }

//        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? new DateTime?() : lockoutEnd.UtcDateTime;
//            return Task.FromResult(0);
//        }

//        public Task SetPasswordHashAsync(TUser user, string passwordHash)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            user.PasswordHash = passwordHash;
//            return Task.FromResult(0);
//        }

//        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            user.PhoneNumber = phoneNumber;
//            return Task.FromResult(0);
//        }

//        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            user.PhoneNumberConfirmed = confirmed;
//            return Task.FromResult(0);
//        }

//        public Task SetSecurityStampAsync(TUser user, string stamp)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            user.SecurityStamp = stamp;
//            return Task.FromResult(0);
//        }

//        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            user.TwoFactorEnabled = enabled;
//            return Task.FromResult(0);
//        }

//        public async Task UpdateAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            _userStore.Update(user);
//            await SaveChanges();
//        }

//        /// <summary>
//        /// Dispose the store
//        /// 
//        /// </summary>
//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        /// <summary>
//        /// If disposing, calls dispose on the Context.  Always nulls out the Context
//        /// 
//        /// </summary>
//        /// <param name="disposing"/>
//        protected virtual void Dispose(bool disposing)
//        {
//            if (DisposeContext && disposing)
//                Context?.Dispose();
//            _disposed = true;
//            Context = null;
//            _userStore = null;
//        }
//        private void ThrowIfDisposed()
//        {
//            if (_disposed)
//                throw new ObjectDisposedException(GetType().Name);
//        }

//        private bool AreClaimsLoaded(TUser user)
//        {
//            return Context.Entry(user).Collection(u => u.Claims).IsLoaded;
//        }

//        private async Task EnsureClaimsLoaded(TUser user)
//        {
//            if (!AreClaimsLoaded(user))
//            {
//                var userId = user.Id;
//                try
//                {
//                    await _userClaims.Where(uc => uc.UserId.Equals(userId)).LoadAsync();
//                    Context.Entry(user).Collection(u => u.Claims).IsLoaded = true;
//                }
//                catch (Exception ex)
//                {

//                }
//            }
//        }

//        private async Task EnsureRolesLoaded(TUser user)
//        {
//            if (!Context.Entry(user).Collection(u => u.Roles).IsLoaded)
//            {
//                var userId = user.Id;
//                await _userRoles.Where(uc => uc.UserId.Equals(userId)).LoadAsync();
//                Context.Entry(user).Collection(u => u.Roles).IsLoaded = true;
//            }
//        }

//        private bool AreLoginsLoaded(TUser user)
//        {
//            return Context.Entry(user).Collection(u => u.Logins).IsLoaded;
//        }

//        private async Task EnsureLoginsLoaded(TUser user)
//        {
//            if (!AreLoginsLoaded(user))
//            {
//                var userId = user.Id;
//                await _logins.Where(uc => uc.UserId.Equals(userId)).LoadAsync();
//                Context.Entry(user).Collection(u => u.Logins).IsLoaded = true;
//            }
//        }

//        //async Task<IList<UserLoginInfo>> IUserLoginStore<TUser, string>.GetLoginsAsync(TUser user)
//        //{
//        //    ThrowIfDisposed();
//        //    if (user == null)
//        //        throw new ArgumentNullException(nameof(user));
//        //    await EnsureLoginsLoaded(user);
//        //    return (IList<UserLoginInfo>)user.Logins.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList();
//        //}

//        private static class FindByIdFilterParser
//        {
//            // ReSharper disable once StaticMemberInGenericType
//            private static readonly Expression<Func<TUser, bool>> Predicate = (u => u.Id.Equals(default(string)));

//            // ReSharper disable once StaticMemberInGenericType
//            private static readonly MethodInfo EqualsMethodInfo =
//                ((MethodCallExpression)Predicate.Body).Method;
//            // ReSharper disable once StaticMemberInGenericType
//            private static readonly MemberInfo UserIdMemberInfo =
//                ((MemberExpression)((MethodCallExpression)Predicate.Body).Object)
//                    ?.Member;

//            internal static bool TryMatchAndGetId(Expression<Func<TUser, bool>> filter, out string id)
//            {
//                id = default(string);
//                if (filter.Body.NodeType != ExpressionType.Call)
//                    return false;
//                MethodCallExpression methodCallExpression = (MethodCallExpression)filter.Body;
//                if (methodCallExpression.Method != UserStore<User>.FindByIdFilterParser.EqualsMethodInfo || methodCallExpression.Object == null || (methodCallExpression.Object.NodeType != ExpressionType.MemberAccess || ((MemberExpression)methodCallExpression.Object).Member != UserStore<User>.FindByIdFilterParser.UserIdMemberInfo) || methodCallExpression.Arguments.Count != 1)
//                    return false;
//                MemberExpression memberExpression;
//                if (methodCallExpression.Arguments[0].NodeType == ExpressionType.Convert)
//                {
//                    UnaryExpression unaryExpression = (UnaryExpression)methodCallExpression.Arguments[0];
//                    if (unaryExpression.Operand.NodeType != ExpressionType.MemberAccess)
//                        return false;
//                    memberExpression = (MemberExpression)unaryExpression.Operand;
//                }
//                else
//                {
//                    if (methodCallExpression.Arguments[0].NodeType != ExpressionType.MemberAccess)
//                        return false;
//                    memberExpression = (MemberExpression)methodCallExpression.Arguments[0];
//                }
//                if (memberExpression.Member.MemberType != MemberTypes.Field || memberExpression.Expression.NodeType != ExpressionType.Constant)
//                    return false;
//                FieldInfo fieldInfo = (FieldInfo)memberExpression.Member;
//                object obj = ((ConstantExpression)memberExpression.Expression).Value;
//                id = (string)fieldInfo.GetValue(obj);
//                return true;
//            }
//        }

//        public async Task AddToRoleAsync(TUser user, string roleName)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            if (string.IsNullOrWhiteSpace(roleName))
//                throw new ArgumentException("Role value cannot be null", nameof(roleName));
//            RoleEntity roleEntity = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper());
//            if (roleEntity == null)
//                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Role not found"));
//            Role instance = Activator.CreateInstance<Role>();
//            instance.UserId = user.Id;
//            instance.RoleId = roleEntity.Id;
//            Role ur = instance;
//            _userRoles.Add(ur);
//        }

//        public async Task RemoveFromRoleAsync(TUser user, string roleName)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            if (string.IsNullOrWhiteSpace(roleName))
//                throw new ArgumentException("Role value cannot be null", nameof(roleName));
//            RoleEntity roleEntity = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper());
//            if (roleEntity != null)
//            {
//                var roleId = roleEntity.Id;
//                var userId = user.Id;
//                Role userRole = await _userRoles.FirstOrDefaultAsync(r => roleId.Equals(r.RoleId) && r.UserId.Equals(userId));
//                if (userRole != null)
//                    _userRoles.Remove(userRole);
//            }
//        }

//        public async Task<IList<string>> GetRolesAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            var userId = user.Id;
//            IQueryable<string> query =
//                _userRoles.Where(userRole => userRole.UserId.Equals(userId))
//                    .Join(_roleStore.DbEntitySet, userRole => userRole.RoleId.ToString(),
//                        role => role.Id.ToString(), (userRole, role) => role.Name);
//            return (IList<string>)await query.ToListAsync();
//        }

//        public async Task<bool> IsInRoleAsync(TUser user, string roleName)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));
//            if (string.IsNullOrWhiteSpace(roleName))
//                throw new ArgumentException("Role value cannot be null", nameof(roleName));
//            RoleEntity role = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper());
//            bool flag;
//            if (role != null)
//            {
//                var userId = user.Id;
//                var roleId = role.Id;
//                flag = await _userRoles.AnyAsync(ur => ur.RoleId.Equals(roleId) && ur.UserId.Equals(userId));
//            }
//            else
//                flag = false;
//            return flag;
//        }

//    }
//}
