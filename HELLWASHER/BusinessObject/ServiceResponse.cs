using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null!;
    }
    public class PaginationModel<T>
    {
        public int Page { get; set; }
        public int TotalPage { get; set; }
        public int TotalRecords { get; set; }
        public List<T> ListData { get; set; }
    }
    public class TokenResponse<T>
    {
        public string? Role { get; set; } = null;
        public bool Success { get; set; } = true;
        public string? Message { get; set; } = null;
        public string? Error { get; set; } = null;
        public string? Hint { get; set; } = null;
        public int? HintId { get; set; } = null;
        public string? Code { get; set; } = null;
        public T AccessToken { get; set; }

        public T RefreshToken { get; set; }
        public List<string>? ErrorMessages { get; set; } = null;
    }
}
