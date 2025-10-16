using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.UI.Gateways
{
    /// <summary>
    /// NengaBoosterの設定ファイルのjsonに対応したDTOオブジェクト。
    /// </summary>
    sealed class NengaBoosterConfigDTO
    {
        public int WaitTime_Naire_DialogOpen { get; set; } = 0;
        public int WaitTime_Naire_DialogOpenOnSaikumi { get; set; } = 0;
        public int WaitTime_Naire_SaikumiAlertDialogOpen { get; set; } = 0;
        public int WaitTime_Hensyu_DialogOpen { get; set; } = 0;
        public int WaitTime_Kouseishi_NextToibanSend { get; set; } = 0;

        public string ProcessName_NengaMenu { get; set; } = string.Empty;  
        public string ProcessName_Naire { get; set; } = string.Empty;
        public string ProcessName_Hensyu { get; set; } = string.Empty;
        public string ProcessName_Information { get; set; } = string.Empty;
        public string ProcessName_Kouseishi { get; set; } = string.Empty;

        public string ApplicationNameOnNengaMenu_Naire { get; set; } = string.Empty;
        public string ApplicationNameOnNengaMenu_Hensyu { get; set; } = string.Empty;
        public string ApplicationNameOnNengaMenu_Information { get; set; } = string.Empty;
        public string ApplicationNameOnNengaMenu_Kouseishi { get; set; } = string.Empty;

        public string WindowTitlePattern_NengaMenu { get; set; } = string.Empty;
        public string WindowTitlePattern_Naire { get; set; } = string.Empty;
        public string WindowTitlePattern_Hensyu { get; set; } = string.Empty;
        public string WindowTitlePattern_Information { get; set; } = string.Empty;
        public string WindowTitlePattern_InformationDetail { get; set; } = string.Empty;
        public string WindowTitlePattern_Kouseishi { get; set; } = string.Empty;
        public string WindowTitlePattern_LoginForm { get; set; } = string.Empty;

        public int WindowWidth_NengaMenu { get; set; } = 0;
        public int WindowWidth_Naire { get; set; } = 0;
        public int WindowWidth_Hensyu { get; set; } = 0;
        public int WindowWidth_Information { get; set; } = 0;
        public int WindowWidth_InformationDetail { get; set; } = 0;
        public int WindowWidth_Kouseishi { get; set; } = 0;
        public int WindowWidth_LoginForm { get; set; } = 0;

        public string DialogTitlePattern_Naire { get; set; } = string.Empty;
        public string DialogTitlePattern_Hensyu { get; set; } = string.Empty;
        public string DialogTitlePattern_Information { get; set; } = string.Empty;
        public string DialogTitlePattern_Kouseishi { get; set; } = string.Empty;

        public int DialogWidth_Naire { get; set; } = 0;
        public int DialogWidth_Hensyu { get; set; } = 0;
        public int DialogWidth_Information { get; set; } = 0;
        public int DialogWidth_Kouseishi { get; set; } = 0;

        public string TextBoxPoint_Naire_Toiban { get; set; } = "0, 0";
        public string ButtonName_Naire_Open { get; set; } = string.Empty;
        public string ButtonName_Naire_Save { get; set; } = string.Empty;
        public string ButtonName_Naire_Close { get; set; } = string.Empty;
        public string RadioButtonName_Naire_KumihanIrai { get; set; } = string.Empty;

        public string TextBoxPoint_Hensyu_Toiban { get; set; } = "0, 0";
        public string ButtonName_Hensyu_Open { get; set; } = string.Empty;
        public string ButtonName_Hensyu_Tegumi { get; set; } = string.Empty;
        public string ButtonName_Hensyu_Close { get; set; } = string.Empty;

        public string TextBoxPoint_Information_Toiban { get; set; } = "0, 0";
        public string ButtonName_Information_Open { get; set; } = string.Empty;
        public string ButtonName_Information_Close { get; set; } = string.Empty;
        public string ButtonName_Information_Detail { get; set; } = string.Empty;

        public string ButtonName_InformationDetail_KouseiPage { get; set; } = string.Empty;
        public string ButtonName_InformationDetail_KumihanPage { get; set; } = string.Empty;
        public string ButtonName_InformationDetail_Close { get; set; } = string.Empty;

        public string TextBoxPoint_Kouseishi_Toiban { get; set; } = "0, 0";
        public string ButtonName_Kouseishi_Close { get; set; } = string.Empty;

        public int TextBoxIndex_LoginForm_UserName { get; set; } = 0;
        public int TextBoxIndex_LoginForm_Password { get; set; } = 0;
        public string TextBoxPoint_LoginForm_UserName { get; set; } = "0, 0";
        public string TextBoxPoint_LoginForm_Password { get; set; } = "0, 0";
        public string ButtonName_LoginForm_Ok { get; set; } = string.Empty;

        public int LabelIndex_Dialog_Message { get; set; } = 0;
        public string LabelPoint_Dialog_Message { get; set; } = "0, 0";
        public string ButtonName_Dialog_Ok { get; set; } = string.Empty;

        public string Texts_Dialog_WorkProcessNames { get; set; } = string.Empty;
        public string Texts_NaireDialog_ErrorMessage { get; set; } = string.Empty;
        public string Texts_HensyuDialog_ErrorMessage { get; set; } = string.Empty;
        public string Texts_InformationDialog_ErrorMessage { get; set; } = string.Empty;
        public string Texts_KouseishiDialog_ErrorMessage { get; set; } = string.Empty;

        public string Path_UserSettingsFile { get; set; } = @"./user_config.json";
        public string Path_KeyReplacerExeFile { get; set; } = @"./KeyReplacer.exe";
        public string Texts_SSSExecutionPeriods { get; set; } = "0900-1130,1215-1430,1445-1700,1710-2100";
    }
}
