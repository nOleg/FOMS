using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
 
namespace DMZPage.Models
{
    public class DMZDBContext : DbContext
    {
        public DMZDBContext(DbContextOptions options) : base(options) { } 
        
        public DbSet<Workflow> Workflow{get;set;}
        public DbSet<RequestSelect> RequestSelect{get;set;}

        public DbSet<PositionReference> PositionReference{get;set;}

        public DbSet<LogStore> LogStore{get;set;}
    }


public class BaseClass{

public List<Workflow> Workflows{get;set;}
public List<RequestSelect> RequestSelects{get;set;}

}

public class Workflow{
       [Key]
        public int RowID{get;set;}
        public string InputFileName{get;set;}
        public System.DateTime InputTime{get;set;}
        public int? TotalSNILS{get;set;}
        public string OutputFileName{get;set;}
        public System.DateTime? OutputTime{get;set;}
        public System.DateTime DateBegin{get;set;}
        public System.DateTime DateEnd{get;set;}
        public System.Byte Status{get;set;}
    //CONSTRAINT [PK_Workflow] PRIMARY KEY CLUSTERED ([RowID] ASC)

}


public class RequestSelect{
     [Key]
     public int RowID{get;set;}
     public int?  Line{get;set;}
    [Required]
    public string  SNILS{get;set;}//CHAR (11)
    [Required]
    public int WorkFlowID{get;set;}
    public System.DateTime? RequestTime{get;set;}
    public System.DateTime?  ResponseTime{get;set;}
    public int? RequestAttempt{get;set;}
    public byte? RequestStatus{get;set;}
    public string StatusMessage{get;set;}
    public int? EmployeeRecords{get;set;}

    // CONSTRAINT [PK_RequestSelect] PRIMARY KEY CLUSTERED ([RowID] ASC),
    // CONSTRAINT [FK_RequestSelect_Workflow] FOREIGN KEY ([WorkFlowID]) REFERENCES [dbo].[Workflow] ([RowID])
}

public class LogStore{
    public string Category{get;set;}
    public System.Int16 CategoryNumber{get;set;}
    public int EventID{get;set;}
    [Key]
    [Required]
    public int Index{get;set;}
    public double InstanceId{get;set;}
    public string MachineName{get;set;}
    public string  Message{get;set;}
    public string  Source{get;set;}
     public System.DateTime? TimeGenerated{get;set;}
    public System.DateTime? TimeWritten{get;set;}
    public string  UserName{get;set;}
    //PRIMARY KEY CLUSTERED ([Index] ASC)
}


public class PositionReference{
    [Key]
    public int RowID{get;set;}
    [Required]
    public int Code{get;set;}
    public int? Parent{get;set;}
    public System.Boolean? Certification{get;set;}
    public string Position{get;set;}
    public System.Boolean? Medical{get;set;}
    public System.Byte? MedCat{get;set;}
    public System.Byte? Enrolling{get;set;}

    public System.DateTime? DateEnd{get;set;}
}

}

