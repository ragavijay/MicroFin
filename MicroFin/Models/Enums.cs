using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroFin.Models
{
    public enum EGender
    {
        Female, TG
    }
    public enum EMaritalStatus
    {
        Married, Widow, Divorced, Unmarried
    }
    public enum EReligion
    {
        Hindu, Muslim, Christian
    }
    public enum EMemberType
    {
        Member, Leader
    }
    public enum EHouseType
    {
        ColonyHouse, Thatched, RCC, Tiled
    }
    public enum EPropertyOwnership
    {
        Self, Rented, Other
    }
    public enum EOccupation
    {
        Agriculture, Manufacturing, Service, Trading
    }
    public enum EOccupationType
    {
        SelfEmployed, Salaried, None
    }
}