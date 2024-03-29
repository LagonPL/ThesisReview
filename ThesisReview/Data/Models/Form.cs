﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ThesisReview.Data.Models
{
  public class Form
  {
    public int FormId { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    [Required]
    public string ReviewType { get; set; }

    [Required]
    [DataType(DataType.MultilineText)]
    public string ShortDescription { get; set; }

    public string Status { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string StudentMail { get; set; }

    [Required]
    public string StudentName { get; set; }

    [Required]
    public string Department { get; set; }

    [Required]
    public string FieldOfStudy { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string GuardianName { get; set; }

    public UserList GuardianUser { get; set; }

    public UserList ReviewerUser { get; set; }

    [DataType(DataType.EmailAddress)]
    public string ReviewerName { get; set; }

    public string FormURL { get; set; }

    public string Link { get; set; }

    public string Password { get; set; }

    [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DateTimeStart { get; set; }

    [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DateTimeFinish { get; set; }

    public Questions Questions { get; set; }

    public Questions QuestionsGuardian { get; set; }

    public byte[] ThesisFile { get; set; }
  }
}
