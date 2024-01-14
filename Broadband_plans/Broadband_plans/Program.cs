using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;
interface IBroadbandPlan
{
    // your code goes here
    int GetBroadbandPlanAmount();
    //{
       // return 0; There is no return for function inside Interface.
    //}
}

class Black : IBroadbandPlan
{
    // your code goes here

    private readonly bool _isSubscriptionValid;     // set later true/false

    private readonly int _discountPercentage;

    private const int PlanAmount = 3000;

    public Black(bool isSusbcriptionValid, int discountPercentage)
    {
        _isSubscriptionValid = isSusbcriptionValid; 
        _discountPercentage = discountPercentage;

        if (_discountPercentage<0 && _discountPercentage>50)
        {
            throw new ArgumentOutOfRangeException();
        }

    }

    public int GetBroadbandPlanAmount()
    {

        if (_isSubscriptionValid==true)
        {
            return (PlanAmount-(_discountPercentage * PlanAmount)/100);
        }
        else
        {
            return PlanAmount;
        }
    }
}


class Gold : IBroadbandPlan
{
    // your code goes here
    private readonly bool _isSubscriptionValid;

    private readonly int _discountPercentage;

    private const int PlanAmount = 1500;

    public Gold(bool isSusbcriptionValid, int discountPercentage)
    {
        _isSubscriptionValid = isSusbcriptionValid;
        _discountPercentage = discountPercentage;

        if (_discountPercentage < 0 && _discountPercentage > 30)
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    public int GetBroadbandPlanAmount()
    {
        if (_isSubscriptionValid == true)
        {
            return (PlanAmount-(_discountPercentage * PlanAmount) / 100);
        }
        else
        {
            return PlanAmount;
        }
    }
}

class SubscribePlan
{
    // your code goes here
    private readonly IList<IBroadbandPlan> _broadbandPlans;       //  _broadbandPlans -> customer field/member/variable : (customer input) -> It is a variable/member, so it (_broadbandPlans) is used to initialize constructor property name (broadbandPlans). 

    public SubscribePlan(IList<IBroadbandPlan> broadbandPlans)          // constructor (same name as class) so we need to initialize the constructor -> broadbandPlans 
    {
        _broadbandPlans = broadbandPlans;       // initialize the customer field -> _broadbandPlans; It is adding IList<IBroadbandPlan> --> broadbandPlans (name of Ilist, containing class Black and Gold) to input --> _broadbandPlans.

        if (_broadbandPlans == null)         // if customer field -> _broadbandPlans is null, tat means no input tan return exception
        {
            throw new ArgumentNullException();
        }
    }

    public IList<Tuple<string, int>> GetSubscriptionPlan()
    {
        if (_broadbandPlans == null)
        {
            throw new ArgumentNullException();
        }

        var result = new List<Tuple<string, int>>();

        foreach (var plan in _broadbandPlans)
        {
            result.Add(new Tuple<string, int>(plan.GetType().Name, plan.GetBroadbandPlanAmount()));
            // OR
            //Tuple<string, int> t = Tuple.Create(plan.ToString(), plan.GetBroadbandPlanAmount());
            //result.Add(t);

        }
        return result;
    }
}

class Source
{
    static void Main(string[] args)
    {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT */
        var plans = new List<IBroadbandPlan>
	      {
	        new Black(true, 50),
	        new Black(false, 10),
	        new Gold(true, 30),
	        new Black(true, 20),
	        new Gold(false, 20)
	      };
	      var subscriptionPlans = new SubscribePlan(plans);
	      var result = subscriptionPlans.GetSubscriptionPlan();
	      foreach (var item in result)
	      {
	        Console.WriteLine($"{item.Item1}, {item.Item2}");
          }

    }
}
