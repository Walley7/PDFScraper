using PDFScrape.Macros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PDFScraper.Forms.Macros {

    public partial class MacroRecordingNextBranchForm : Form {
        //================================================================================
        


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroRecordingNextBranchForm() {
            // Initialise
            InitializeComponent();
        }


        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void MacroRecordingNextBranchForm_Shown(object sender, EventArgs e) {
            // Conditions
            string[] conditions = MacroBranchNode.ConditionStringList();
            cboCondition.Items.AddRange(conditions);
        }


        // VALIDATION ================================================================================
        //--------------------------------------------------------------------------------
        private void btnOk_Click(object sender, EventArgs e) {
            // Dialog result
            this.DialogResult = DialogResult.None;
            
            // Validation
            if (cboCondition.SelectedIndex == -1) {
                MessageBox.Show("Please select a condition.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboCondition.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtConditionArgument.Text) &&                
                ((Condition == MacroBranchNode.ConditionType.EQUALS) || (Condition == MacroBranchNode.ConditionType.GREATER_THAN) ||
                 (Condition == MacroBranchNode.ConditionType.GREATER_THAN_OR_EQUAL) || (Condition == MacroBranchNode.ConditionType.LESS_THAN) ||
                 (Condition == MacroBranchNode.ConditionType.LESS_THAN_OR_EQUAL) || (Condition == MacroBranchNode.ConditionType.CONTAINS) ||
                 (Condition == MacroBranchNode.ConditionType.DOES_NOT_CONTAIN) || (Condition == MacroBranchNode.ConditionType.STARTS_WITH) ||
                 (Condition == MacroBranchNode.ConditionType.DOES_NOT_START_WITH)))
            {
                MessageBox.Show("Please enter a condition argument.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtConditionArgument.Focus();
                return;
            }

            // Validated
            this.DialogResult = DialogResult.OK;
        }
        

        // CONTROLS ================================================================================
        //--------------------------------------------------------------------------------
        private void cboCondition_SelectedIndexChanged(object sender, EventArgs e) {
            txtConditionArgument.Enabled = (Condition != MacroBranchNode.ConditionType.ALWAYS) && (Condition != MacroBranchNode.ConditionType.IS_AN_INTEGER) &&
                                           (Condition != MacroBranchNode.ConditionType.IS_NOT_AN_INTEGER) && (Condition != MacroBranchNode.ConditionType.IS_A_DECIMAL) &&
                                           (Condition != MacroBranchNode.ConditionType.IS_NOT_A_DECIMAL) && (Condition != MacroBranchNode.ConditionType.IS_A_CURRENCY) &&
                                           (Condition != MacroBranchNode.ConditionType.IS_NOT_A_CURRENCY);
            if (!txtConditionArgument.Enabled)
                txtConditionArgument.Text = "";
        }


        // FIELDS ================================================================================
        //--------------------------------------------------------------------------------
        public MacroBranchNode.ConditionType Condition { get { return (MacroBranchNode.ConditionType)cboCondition.SelectedIndex; } }
        public string ConditionArgument { get { return txtConditionArgument.Text; } }
        public bool Final { get { return chkFinal.Checked; } }
    }

}
