//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SDE_TimeTracking.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkingTime
    {
        public int EmployeeID { get; set; }
        public System.DateTime TimeStart { get; set; }
        public Nullable<System.DateTime> TimeEnd { get; set; }
    
        public virtual Employees Employees { get; set; }
    }
}
