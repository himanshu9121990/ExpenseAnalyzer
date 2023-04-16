using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Table("HDFC_SB_Statement")]
    public class HdfcSbStatement
    {
        /// <summary>
        /// Tag Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Date of transaction
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Narration of transaction
        /// </summary>
        public string Narration { get; set; }
        /// <summary>
        /// Cheque or ref no of transaction
        /// </summary>
        public string ChequeOrRefNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ValueDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? WithdrawalAmount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DepositAmount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal ClosingBalance { get; set; }

    }
}
