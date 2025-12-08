using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Gateways
{
    /// <summary>
    /// NengaBoosterの設定のDTOクラスをConfigクラスに変換するクラス。
    /// </summary>
    sealed class NengaBoosterConfigDtoMapper
    {
        private readonly ConfigValueParser _configValueParser;


        public NengaBoosterConfigDtoMapper(ConfigValueParser configValueParser)
        {
            Assert.IsNull(configValueParser, nameof(configValueParser));

            _configValueParser = configValueParser;
        }


        /// <summary>
        /// 年賀メニューのコンフィグに変換する。
        /// </summary>
        public NengaMenuConfig ConvertToNengaMenuConfigFrom(NengaBoosterConfigDTO dto)
        {
            return new NengaMenuConfig
            {
                Basic = new ApplicationBasicConfig
                {
                    ProcessName = dto.ProcessName_NengaMenu,
                    WindowTitlePattern = dto.WindowTitlePattern_NengaMenu,
                    WindowWidth = dto.WindowWidth_NengaMenu
                }
            };
        }

        /// <summary>
        /// 注文・名入れのコンフィグに変換する。
        /// </summary>
        public NaireApplicationConfig ConvertToNaireApplicationConfigFrom(NengaBoosterConfigDTO dto)
        {
            return new NaireApplicationConfig
            {
                Basic = new ApplicationBasicConfig
                {
                    ProcessName = dto.ProcessName_Naire,
                    ApplicationName_OnNengaMenu = dto.ApplicationNameOnNengaMenu_Naire,
                    WindowTitlePattern = dto.WindowTitlePattern_Naire,
                    WindowWidth = dto.WindowWidth_Naire,
                },

                DialogTitlePattern = dto.DialogTitlePattern_Naire,
                DialogWidth = dto.DialogWidth_Naire,

                Texts_Dialog_ErrorMessage = dto.Texts_NaireDialog_ErrorMessage.Split(',').ToList(),

                TextBoxPoint_Toiban = _configValueParser.ToObjectFrom<Point>(dto.TextBoxPoint_Naire_Toiban),
                ButtonName_Open = dto.ButtonName_Naire_Open,
                ButtonName_Save = dto.ButtonName_Naire_Save,
                ButtonName_Close = dto.ButtonName_Naire_Close,
                RadioButtonName_KumihanIrai = dto.RadioButtonName_Naire_KumihanIrai,
            };
        }

        public NaireBehaviorConfig ConvertToNaireBehaviorConfig(NengaBoosterConfigDTO dto)
        {
            return new NaireBehaviorConfig
            {
                WaitTime_DialogOpen = dto.WaitTime_Naire_DialogOpen,
                WaitTime_DialogOpenOnSaikumi = dto.WaitTime_Naire_DialogOpenOnSaikumi,
                WaitTime_SaikumiAlertDialogOpen = dto.WaitTime_Naire_SaikumiAlertDialogOpen,

                Texts_Dialog_WorkProcessNames = dto.Texts_Dialog_WorkProcessNames.Split(',').ToList(),
            };
        }

        /// <summary>
        /// 編集のコンフィグに変換する。
        /// </summary>
        public HensyuApplicationConfig ConvertToHensyuApplicationConfigFrom(NengaBoosterConfigDTO dto)
        {
            return new HensyuApplicationConfig
            {
                Basic = new ApplicationBasicConfig
                {
                    ProcessName = dto.ProcessName_Hensyu,
                    ApplicationName_OnNengaMenu = dto.ApplicationNameOnNengaMenu_Hensyu,
                    WindowTitlePattern = dto.WindowTitlePattern_Hensyu,
                    WindowWidth = dto.WindowWidth_Hensyu,
                },

                DialogTitlePattern = dto.DialogTitlePattern_Hensyu,
                DialogWidth = dto.DialogWidth_Hensyu,
                Texts_Dialog_ErrorMessage = dto.Texts_HensyuDialog_ErrorMessage.Split(',').ToList(),

                TextBoxPoint_Toiban = _configValueParser.ToObjectFrom<Point>(dto.TextBoxPoint_Hensyu_Toiban),
                ButtonName_Open = dto.ButtonName_Hensyu_Open,
                ButtonName_Tegumi = dto.ButtonName_Hensyu_Tegumi,
                ButtonName_Close = dto.ButtonName_Hensyu_Close,
            };
        }

        public HensyuBehaviorConfig ConvertToHensyuBehaviorConfig(NengaBoosterConfigDTO dto)
        {
            return new HensyuBehaviorConfig
            {
                WaitTime_DialogOpen = dto.WaitTime_Naire_DialogOpen,

                Texts_Dialog_WorkProcessNames = dto.Texts_Dialog_WorkProcessNames.Split(',').Select(t => t.Trim()).ToList(),
            };
        }

        /// <summary>
        /// インフォメーションのコンフィグに変換する。
        /// </summary>
        public InformationApplicationConfig ConvertToInformationApplicationConfigFrom(NengaBoosterConfigDTO dto)
        {
            return new InformationApplicationConfig
            {
                Basic = new ApplicationBasicConfig
                {
                    ProcessName = dto.ProcessName_Information,
                    ApplicationName_OnNengaMenu = dto.ApplicationNameOnNengaMenu_Information,

                    WindowTitlePattern = dto.WindowTitlePattern_Information,
                    WindowWidth = dto.WindowWidth_Information,
                },

                DialogTitlePattern = dto.DialogTitlePattern_Information,
                DialogWidth = dto.DialogWidth_Information,
                Texts_Dialog_ErrorMessage = dto.Texts_InformationDialog_ErrorMessage.Split(',').ToList(),

                TextBoxPoint_Toiban = _configValueParser.ToObjectFrom<Point>(dto.TextBoxPoint_Information_Toiban),
                ButtonName_Open = dto.ButtonName_Information_Open,
                ButtonName_Close = dto.ButtonName_Information_Close,
                ButtonName_Detail = dto.ButtonName_Information_Detail
            };
        }

        /// <summary>
        /// インフォメーション詳細のコンフィグに変換する。
        /// </summary>
        public InformationDetailApplicationConfig ConvertToInformationDetailApplicationConfigFrom(NengaBoosterConfigDTO dto)
        {
            return new InformationDetailApplicationConfig
            {
                Basic = new ApplicationBasicConfig
                {
                    ApplicationName_OnNengaMenu = String.Empty,
                    ProcessName = String.Empty,

                    WindowTitlePattern = dto.WindowTitlePattern_InformationDetail,
                    WindowWidth = dto.WindowWidth_InformationDetail,
                },

                ButtonName_KouseiPage = dto.ButtonName_InformationDetail_KouseiPage,
                ButtonName_KumihanPage = dto.ButtonName_InformationDetail_KumihanPage,
                ButtonName_Close = dto.ButtonName_InformationDetail_Close
            };
        }

        /// <summary>
        /// 校正紙出力のコンフィグに変換する。
        /// </summary>
        public KouseishiApplicationConfig ConvertToKouseishiApplicationConfigFrom(NengaBoosterConfigDTO dto)
        {
            return new KouseishiApplicationConfig
            {
                Basic = new ApplicationBasicConfig
                {
                    ProcessName = dto.ProcessName_Kouseishi,
                    ApplicationName_OnNengaMenu = dto.ApplicationNameOnNengaMenu_Kouseishi,

                    WindowTitlePattern = dto.WindowTitlePattern_Kouseishi,
                    WindowWidth = dto.WindowWidth_Kouseishi,
                },

                DialogTitlePattern = dto.DialogTitlePattern_Kouseishi,
                DialogWidth = dto.DialogWidth_Kouseishi,
                Texts_Dialog_ErrorMessage = dto.Texts_KouseishiDialog_ErrorMessage.Split(',').ToList(),

                TextBoxPoint_Toiban = _configValueParser.ToObjectFrom<Point>(dto.TextBoxPoint_Kouseishi_Toiban),
                ButtonName_Close = dto.ButtonName_Kouseishi_Close
            };
        }

        public KouseishiBehaviorConfig ConvertToKouseishiBehaviorConfig(NengaBoosterConfigDTO dto)
        {
            return new KouseishiBehaviorConfig
            {
                //WaitTime_NextToibanSend = dto.WaitTime_Kouseishi_NextToibanSend
            };
        }

        /// <summary>
        /// ログインフォームのコンフィグに変換する。
        /// </summary>
        public LoginFormConfig ConvertToLoginFormConfigFrom(NengaBoosterConfigDTO dto)
        {
            return new LoginFormConfig
            {
                Basic = new ApplicationBasicConfig
                {
                    ApplicationName_OnNengaMenu = String.Empty,
                    ProcessName = String.Empty,

                    WindowTitlePattern = dto.WindowTitlePattern_LoginForm,
                    WindowWidth = dto.WindowWidth_LoginForm,
                },

                TextBoxIndex_UserName = dto.TextBoxIndex_LoginForm_UserName,
                TextBoxIndex_Password = dto.TextBoxIndex_LoginForm_Password,
                TextBoxPoint_UserName = _configValueParser.ToObjectFrom<Point>(dto.TextBoxPoint_LoginForm_UserName),
                TextBoxPoint_Password = _configValueParser.ToObjectFrom<Point>(dto.TextBoxPoint_LoginForm_Password),
                ButtonName_Ok = dto.ButtonName_LoginForm_Ok
            };
        }

        public DialogConfig ConvertToNaireDialogConfigFrom(NengaBoosterConfigDTO dto)
        {
            return ConvertToDialogConfig(
                dto.DialogTitlePattern_Naire,
                dto.DialogWidth_Naire,
                dto.Texts_NaireDialog_ErrorMessage,
                dto);
        }

        public DialogConfig ConvertToHensyuDialogConfigFrom(NengaBoosterConfigDTO dto)
        {
            return ConvertToDialogConfig(
                dto.DialogTitlePattern_Hensyu,
                dto.DialogWidth_Hensyu,
                dto.Texts_HensyuDialog_ErrorMessage,
                dto);
        }

        public DialogConfig ConvertToInformationDialogConfigFrom(NengaBoosterConfigDTO dto)
        {
            return ConvertToDialogConfig(
                dto.DialogTitlePattern_Information,
                dto.DialogWidth_Information,
                dto.Texts_InformationDialog_ErrorMessage,
                dto);
        }

        public DialogConfig ConvertToKouseishiDialogConfigFrom(NengaBoosterConfigDTO dto)
        {
            return ConvertToDialogConfig(
                dto.DialogTitlePattern_Kouseishi,
                dto.DialogWidth_Kouseishi,
                string.Empty,
                dto);
        }

        /// <summary>
        /// ダイアログのコンフィグに変換する。
        /// </summary>
        private DialogConfig ConvertToDialogConfig(string title, int width, string eMsg, NengaBoosterConfigDTO dto)
        {
            return new DialogConfig
            {
                Basic = new ApplicationBasicConfig
                {
                    ApplicationName_OnNengaMenu = String.Empty,
                    ProcessName = String.Empty,

                    WindowTitlePattern = title,
                    WindowWidth = width,
                },

                LabelIndex_Message = dto.LabelIndex_Dialog_Message,
                LabelPoint_Message = _configValueParser.ToObjectFrom<Point>(dto.LabelPoint_Dialog_Message),
                ButtonName_Ok = dto.ButtonName_Dialog_Ok,

                Text_MovedForwardWorkProcessMessage = dto.Text_Dialog_MovedForwardWorkProcessMessage,

                Texts_ErrorMessage = eMsg.Split(',').Select(t => t.Trim()).ToArray(),
            };
        }

        /// <summary>
        /// その他のコンフィグに変換する。
        /// </summary>
        public OtherConfig ConvertToOtherConfig(NengaBoosterConfigDTO dto)
        {
            return new OtherConfig
            {
                Path_UserSettingsFile = dto.Path_UserSettingsFile,
                Path_KeyReplacerExeFile = dto.Path_KeyReplacerExeFile,
                Texts_SSSExecutionPeriods = dto.Texts_SSSExecutionPeriods.Split(',')
                    .Select(period => _configValueParser.ToObjectFrom<(DateTime, DateTime)>(period))
                    .ToList()
            };
        }


        ///// <summary>
        ///// NengaBoosterの設定のDTOクラスをConfigクラスに変換する。
        ///// </summary>
        //public IsTama.NengaBooster.Core.Configs.NengaBoosterConfig ConvertToNengaBoosterConfig(NengaBoosterConfigDTO dto)
        //{
        //    var nengaMenuConfig = ConvertToNengaMenuConfig(dto);
        //
        //    var naireApplicationConfig = ConvertToNaireApplicationConfig(dto);
        //    var naireBehaviorConfig = ConvertToNaireBehaviorConfig(dto);
        //
        //    var hensyuApplicatonConfig = ConvertToHensyuApplicationConfig(dto);
        //    var hensyuBehaviorConfig = ConvertToHensyuBehaviorConfig(dto);
        //
        //    var informationApplicationConfig = ConvertToInformationApplicationConfig(dto);
        //    var informationDetailApplicationConfig = ConvertToInformationDetailApplicationConfig(dto);
        //
        //    var kouseishiApplicationConfig = ConvertToKouseishiApplicationConfig(dto);
        //    var kouseishiBehaviorConfig = ConvertToKouseishiBehaviorConfig(dto);
        //
        //    var loginFormConfig = ConvertToLoginFormConfig(dto);
        //    var dialogConfig = ConvertToDialogConfig(dto);
        //    var otherConfig = ConvertToOtherConfig(dto);
        //
        //    return new Core.Configs.NengaBoosterConfig
        //    {
        //        NengaMenu = nengaMenuConfig,
        //
        //        NaireApplication = naireApplicationConfig,
        //        NaireBehavior = naireBehaviorConfig,
        //
        //        HensyuApplication = hensyuApplicatonConfig,
        //        HensyuBehavior = hensyuBehaviorConfig,
        //
        //        InformationApplication = informationApplicationConfig,
        //        InformationDetailApplication = informationDetailApplicationConfig,
        //
        //        KouseishiApplication = kouseishiApplicationConfig,
        //        KouseishiBehavior = kouseishiBehaviorConfig,
        //
        //        LoginForm = loginFormConfig,
        //        Dialog = dialogConfig,
        //        Other = otherConfig
        //    };
        //}

        ///// <summary>
        ///// NengaBoosterConfigDTOに変換する。
        ///// </summary>
        //public NengaBoosterConfigDTO ConvertToNengaBoosterConfigDTO(Core.Configs.NengaBoosterConfig config)
        //{
        //    return new NengaBoosterConfigDTO
        //    {
        //        WaitTime_Naire_DialogOpen = config.NaireBehavior.WaitTime_DialogOpen,
        //        WaitTime_Naire_DialogOpenOnSaikumi = config.NaireBehavior.WaitTime_DialogOpenOnSaikumi,
        //        WaitTime_Naire_SaikumiAlertDialogOpen = config.NaireBehavior.WaitTime_SaikumiAlertDialogOpen,
        //        WaitTime_Hensyu_DialogOpen = config.Hensyu.WaitTime_DialogOpen,
        //        WaitTime_Kouseishi_NextToibanSend = config.Kouseishi.WaitTime_NextToibanSend,
        //
        //        ProcessName_NengaMenu = config.NengaMenu.Basic.ProcessName,
        //        ProcessName_Naire = config.NaireApplication.Basic.ProcessName,
        //        ProcessName_Hensyu = config.Hensyu.ProcessName,
        //        ProcessName_Information = config.Information.ProcessName,
        //        ProcessName_Kouseishi = config.Kouseishi.ProcessName,
        //
        //        ApplicationName_OnNengaMenu_Naire = config.NaireApplication.Basic.ApplicationName_OnNengaMenu,
        //        ApplicationName_OnNengaMenu_Hensyu = config.Hensyu.ApplicationName_OnNengaMenu,
        //        ApplicationName_OnNengaMenu_Information = config.Information.ApplicationName_OnNengaMenu,
        //        ApplicationName_OnNengaMenu_Kouseishi = config.Kouseishi.ApplicationName_OnNengaMenu,
        //
        //        WindowTitlePattern_NengaMenu = config.NengaMenu.Basic.WindowTitlePattern,
        //        WindowTitlePattern_Naire = config.NaireApplication.Basic.WindowTitlePattern,
        //        WindowTitlePattern_Hensyu = config.Hensyu.WindowTitlePattern,
        //        WindowTitlePattern_Information = config.Information.WindowTitlePattern,
        //        WindowTitlePattern_InformationDetail = config.InformationDetail.WindowTitlePattern,
        //        WindowTitlePattern_Kouseishi = config.Kouseishi.WindowTitlePattern,
        //        WindowTitlePattern_LoginForm = config.LoginForm.Basic.WindowTitlePattern,
        //
        //        WindowWidth_NengaMenu = config.NengaMenu.Basic.WindowWidth,
        //        WindowWidth_Naire = config.NaireApplication.Basic.WindowWidth,
        //        WindowWidth_Hensyu = config.Hensyu.WindowWidth,
        //        WindowWidth_Information = config.Information.WindowWidth,
        //        WindowWidth_InformationDetail = config.InformationDetail.WindowWidth,
        //        WindowWidth_Kouseishi = config.Kouseishi.WindowWidth,
        //        WindowWidth_LoginForm = config.LoginForm.Basic.WindowWidth,
        //
        //        DialogTitlePattern_Naire = config.NaireApplication.DialogTitlePattern,
        //        DialogTitlePattern_Hensyu = config.Hensyu.DialogTitlePattern,
        //        DialogTitlePattern_Information = config.Information.DialogTitlePattern,
        //        DialogTitlePattern_Kouseishi = config.Kouseishi.DialogTitlePattern,
        //
        //        DialogWidth_Naire = config.NaireApplication.DialogWidth,
        //        DialogWidth_Hensyu = config.Hensyu.DialogWidth,
        //        DialogWidth_Information = config.Information.DialogWidth,
        //        DialogWidth_Kouseishi = config.Kouseishi.DialogWidth,
        //
        //        TextBoxPoint_Naire_Toiban = _configValueParser.ToStringFrom(config.NaireApplication.TextBoxPoint_Toiban),
        //        ButtonName_Naire_Open = config.NaireApplication.ButtonName_Open,
        //        ButtonName_Naire_Save = config.NaireApplication.ButtonName_Save,
        //        ButtonName_Naire_Close = config.NaireApplication.ButtonName_Close,
        //        RadioButtonName_Naire_KumihanIrai = config.NaireApplication.RadioButtonName_KumihanIrai,
        //
        //        TextBoxPoint_Hensyu_Toiban = _configValueParser.ToStringFrom(config.Hensyu.TextBoxPoint_Toiban),
        //        ButtonName_Hensyu_Open = config.Hensyu.ButtonName_Open,
        //        ButtonName_Hensyu_Tegumi = config.Hensyu.ButtonName_Tegumi,
        //        ButtonName_Hensyu_Close = config.Hensyu.ButtonName_Close,
        //
        //        TextBoxPoint_Information_Toiban = _configValueParser.ToStringFrom(config.Information.TextBoxPoint_Toiban),
        //        ButtonName_Information_Open = config.Information.ButtonName_Open,
        //        ButtonName_Information_Close = config.Information.ButtonName_Close,
        //        ButtonName_Information_Detail = config.Information.ButtonName_Detail,
        //
        //        ButtonName_InformationDetail_KouseiPage = config.InformationDetail.ButtonName_KouseiPage,
        //        ButtonName_InformationDetail_KumihanPage = config.InformationDetail.ButtonName_KumihanPage,
        //        ButtonName_InformationDetail_Close = config.InformationDetail.ButtonName_Close,
        //
        //        TextBoxPoint_Kouseishi_Toiban = _configValueParser.ToStringFrom(config.Kouseishi.TextBoxPoint_Toiban),
        //        ButtonName_Kouseishi_Close = config.Kouseishi.ButtonName_Close,
        //
        //        TextBoxIndex_LoginForm_UserName = config.LoginForm.TextBoxIndex_UserName,
        //        TextBoxIndex_LoginForm_Password = config.LoginForm.TextBoxIndex_Password,
        //        TextBoxPoint_LoginForm_UserName = _configValueParser.ToStringFrom(config.LoginForm.TextBoxPoint_UserName),
        //        TextBoxPoint_LoginForm_Password = _configValueParser.ToStringFrom(config.LoginForm.TextBoxPoint_Password),
        //        ButtonName_LoginForm_Ok = config.LoginForm.ButtonName_Ok,
        //
        //        LabelIndex_Dialog_Message = config.Dialog.LabelIndex_Message,
        //        LabelPoint_Dialog_Message = _configValueParser.ToStringFrom(config.Dialog.LabelPoint_Message),
        //        ButtonName_Dialog_Ok = config.Dialog.ButtonName_Ok,
        //
        //        Texts_Dialog_WorkProcessNames = string.Join(",", config.NaireBehavior.Texts_Dialog_WorkProcessNames),
        //        Texts_NaireDialog_ErrorMessage = config.NaireApplication.Texts_Dialog_ErrorMessage,
        //        Texts_HensyuDialog_ErrorMessage = config.Hensyu.Texts_Dialog_ErrorMessage,
        //        Texts_InformationDialog_ErrorMessage = config.Information.Texts_Dialog_ErrorMessage,
        //        Texts_KouseishiDialog_ErrorMessage = config.Kouseishi.Texts_Dialog_ErrorMessage,
        //
        //        Path_UserSettingsFile = config.Other.Path_UserSettingsFile,
        //        Path_KeyReplacerExeFile = config.Other.Path_KeyReplacerExeFile,
        //        Texts_SSSExecutionPeriods = string.Join(",", config.Other.Texts_SSSExecutionPeriods.Select(period => _configValueParser.ToStringFrom(period))),
        //    };
        //}
    }
}
