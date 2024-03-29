﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Datahub.Core.Model.Datahub;

public class Project_Resources
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string ResourceType {  get; set; }
    [StringLength(200)]
    public string ResourceName {  get; set; }
    [StringLength(200)]
    public string Attributes {  get; set; }
    public string Param1 { get; set; }
    public string Param2 { get; set; }
    public string Param3 { get; set; }
    public DateTime TimeRequested {  get; set; }
    public DateTime? TimeCreated {  get; set; }

    public Datahub_Project Project { get; set; }
}