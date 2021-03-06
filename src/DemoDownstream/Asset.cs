﻿using System;
using System.ComponentModel.DataAnnotations;
using CcAcca.DemoUpstream;

namespace CcAcca.DemoDownstream
{
    public partial class Asset
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Reference { get; set; }

        public virtual LookupItem AssetType { get; set; }
        public int? AssetTypeKey { get; set; }

        public virtual UserRole RequiredUserRole { get; set; }
        public int? RequiredUserRoleId { get; set; }

        public DateTimeOffset Created { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }
    }
}