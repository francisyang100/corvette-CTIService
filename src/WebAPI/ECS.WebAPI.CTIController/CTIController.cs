using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ECS.Common.Logging;
using log4net;
using ECS.Common.Logging;
using System.Reflection;
using ECS.Common.Logging;
using ECS.ServiceContracts.CTI;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ECS.WebAPI.CTIController
{
    [Route("api/[controller]")]
    public partial class CTIController : BaseController
    {
        private readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Log.Info(new LogObject("EventName", "CTIController Get"));

            return new string[] { "value1", "value2", "value3"};
        }

         
        #region Call History related

        /// <summary>
        /// write a “call start” record to the Call History Table 
        /// Input
        /// {
        ///     “ucid”: 12345,
        ///     “dnis”: 100,
        ///     “ani”: “111-222-3333”
        /// }
        /// </summary>
        [HttpPost]
        [Route("api/CTI/NewCall")]
        public void NewCall([FromBody] NewCallRequest request)
        {

        }

        /// <summary>
        /// Update CallHistorydetail table as the call flows
        /// {
        ///     "ucid": 1000,
        ///     "action": "HANGUP",
        ///     "action_data": "billing",	
        ///     "last_location": 3020	
        /// }
        /// 
        /// action = ‘PROGRESS’, TRANSFER’,’HANGUP’ 
        /// </summary>
        [HttpPut]
        [Route("api/CTI/CallHistory")]
        public void CallHistory([FromBody] CallHistoryUpdateRequest request)
        {

        }
        #endregion Call History Related

        #region Find Customer Related
        [HttpGet]
        [Route("api/CTI/CheckANI")]
        /// <summary>
        /// Find customer by phone number
        /// api/CTI/CheckANI;{filter}
        /// filter:
        ///    ucid=12345,dnis=6031,ani=2000
        /// 
        /// ucid=12345	Universal Call ID for this call session 
        /// dnis=6031	The incoming DNIS for the call – 800 number translation 
        /// ani=7141112222	caller’s number (ANI/DNIS)translation 
        /// </summary>
        /// <returns></returns>
        public IActionResult CheckANI()
        {
            return new EmptyResult();
        }

        [HttpGet]
        [Route("api/CTI/CheckCCNumber")]
        /// <summary>
        ///  Find customer by Credit Card Number
        /// 
        ///  CheckCCNumber takes the UCID and billing credit card number as inputs and returns all customer records that match that credit card
        ///  api/CTI/CheckCustomer;{filter}
        ///  filter:
        ///     ucid=12345,creditcard=MTE1ODgzODgxOQAVt0RCJFAubnkgNUTCiRKcycV+aKYhdYRp174EAA2KqQ==
        /// </summary>
        /// <returns></returns>
        public IActionResult CheckCCNumber()
        {
            return new EmptyResult();
        }

        /// <summary>
        /// Find customer by CustomerId
        /// 
        /// CheckCustomerNumber takes the UCID and a customer number as inputs and returns corresponding customer information
        /// api/CTI/CheckCustomerNumber;{filter}
        /// filter:
        ///     ucid=12345,customerid=10016845933616
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/CTI/CheckCustomerNumber")]
        public IActionResult CheckCustomerNumber()
        {
            return new EmptyResult();
        }

        /// <summary>
        /// Find customer by Last4SSN + Birthdate + Zip
        /// 
        /// api/CTI/GetCustomerData;{filter}
        /// filter:
        ///     ucid=12345,last4ssn=1234,birthdate=01011980,zip=92626
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/CTI/GetCustomerData")]
        public IActionResult GetCustomerData()
        {
            return new EmptyResult();
        }

        #endregion Find Customer Related

        #region Cancel Membership
        /// <summary>
        /// api/CTI/CancelMembership;{filter}
        /// filter:
        /// {
        ///     "ucid": "1000",
        ///     customerid: "10016845933616"
        /// }
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("api/CTI/CancelMembership")]
        public IActionResult CancelMembership()
        {
            return new EmptyResult();
        }

        #endregion Cancel Membership

        /// <summary>
        /// Determine if the password was recently change for the customer either through the web or CTI
        /// api/CTI/LastPwChange;{filter}
        /// filter: 
        ///     ucid=12345,customerid=10016845933616
        /// </summary>
        /// <returns></returns>
        #region Password Reset related
        [HttpGet]
        [Route("api/CTI/LastPwChange")]
        public IActionResult LastPasswordChange()
        {
            return new EmptyResult();
        }

        /// <summary>
        /// api/CTI/ResetPassword;{filter}
        /// filter:
        ///     ucid=12345,customerid=10016845933616
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("api/CTI/ResetPassword")]
        public IActionResult ResetPassword()
        {
            return new EmptyResult();
        }

        #endregion Password Reset related

        #region upsell/downsell offers related

        /// <summary>
        /// Retrieves a retention offer for downsell, if one is available, for a customer.
        /// api/CTI/GetOffers;{filter}
        /// filter:
        ///     ucid=12345,customerid=10016845933616
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/CTI/GetOffers")]
        public IActionResult getOffers()
        {
            return new EmptyResult();
        }

        /// <summary>
        /// Apply the downsell
        /// {
        ///     “ucid”: 12345,
        ///     “offer_id”: 533,
        ///     “membership_change_type”: “upsell”
        /// }
        /// 
        /// </summary>
        [HttpPost]
        [Route("api/CTI/ApplyOffer")]
        public void ApplyOffer()
        {

        }

        #endregion upsell/downsell offers related

        #region Credit Card related

        /// <summary>
        /// Update Credit card.  The one in our system is soft declined
        /// {
        ///     "ucid": "1000",
        ///     "customerid":10016845933616,
        ///     "credit_card":"MTI3NjUwNzczOQA6XU6R+EVk7iIsfhV5PyrP9XqFW7ZEQ8wA0nFIBj3iZ9AxJdu6bnJY7Jr4wV1ixGc=",
        ///     “exp_date”: “2017-01-01”,
        ///     “card_type”: “V”
        /// }
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("api/CTI/UpdateCCNumber")]
        public IActionResult UpdateCCNumber()
        {
            return new EmptyResult();
        }

        #endregion Credit Card related

    }
}
