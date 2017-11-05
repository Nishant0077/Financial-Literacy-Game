using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum PlayerAttributeEnums
{
    creditScore,
    goalAmount,
    budgetingMethod,
    weeklyIncome,
    MonthlySavings,
    taxRate,
    hourlyWage,
    hoursWorkedInWeek,
    // budgeting method------------------
    savingsFactor,
    discretionaryFactor,
    fixedExpensesFactor,
    //-------------------------------
    annualIncome,
    currentSavingsAmount,
    isEnrolledInSchool,   // player prefs doesn't have bool, this is a workaround
    hoursOfEmployment // default for the hours worked in a week
}
