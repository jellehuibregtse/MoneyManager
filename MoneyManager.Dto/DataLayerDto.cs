using MoneyManager.DAL;

namespace MoneyManager.DTO
{
    class DataLayerDto
    {
        public AppDbContext AppDbContext { get; set; }
        public SqlAccountRepository SqlAccountRepository { get; set; }
    }
}
