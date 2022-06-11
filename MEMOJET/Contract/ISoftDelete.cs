using System;

namespace MEMOJET.Contract
{
    public interface ISoftDelete
    {
        bool IsDeleted{ get; set; }
    }
}