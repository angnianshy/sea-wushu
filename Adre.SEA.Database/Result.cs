using System;
using System.ComponentModel.DataAnnotations;
using Adre.Controls;

namespace Adre.SEA.Database
{
  public class Result : IResult
  {
    public Result()
    {
      Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    public float QM { get; set; }

    public float OB { get; set; }

    public float DD { get; set; }

    public float Penalty { get; set; }

    public float FinalScore { get; set; }

    public string Remarks { get; set; }

    public string Medal { get; set; } //angns

    public virtual Match Match { get; set; }
  }
}
