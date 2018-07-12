using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinica.Web.Models.Common
{
    public class MessageModel
    {
        public int severity { get; set; }

        public string message { get; set; }

        public bool isVisible { get; set; }

        public bool isPersistent { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageModel() { }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="severity">Severity: Message Type Enumeration</param>
        /// <param name="message">Text of the messsage</param>
        /// <param name="isVisible">True:Visible,False:Hidden</param>
        public MessageModel(int severity, string message, bool isVisible)
        {
            this.severity = severity;
            this.message = message;
            this.isVisible = isVisible;
        }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="severity">Severity: Message Type Enumeration</param>
        /// <param name="message">Text of the messsage</param>
        /// <param name="isVisible">True:Visible,False:Hidden</param>
        /// <param name="isPersistent">Defines if message is persistent or not on-screen</param>
        public MessageModel(int severity, string message, bool isVisible, bool isPersistent)
        {
            this.severity = severity;
            this.message = message;
            this.isVisible = isVisible;
            this.isPersistent = isPersistent;
        }
    }
}