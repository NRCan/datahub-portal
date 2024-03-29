﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Datahub.Core.Model.Datahub;

public class SystemNotification
{
    [Key]
    public long Notification_ID { get; set; }

    [Required]
    [StringLength(200)]
    public string ReceivingUser_ID { get; set; }

    [Required]
    public DateTime Generated_TS { get; set; }

    public bool Read_FLAG { get; set; }

    [Required]
    public string NotificationTextEn_TXT { get; set; }

    [Required]
    public string NotificationTextFr_TXT { get; set; }

    [StringLength(512)]
    public string ActionLink_URL { get; set; }

    [StringLength(128)]
    public string ActionLink_Key {  get; set; }

}