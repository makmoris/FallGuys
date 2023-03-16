#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>
#include <stdint.h>


template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
struct InterfaceActionInvoker0
{
	typedef void (*Action)(void*, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R>
struct InterfaceFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R, typename T1>
struct InterfaceFuncInvoker1
{
	typedef R (*Func)(void*, T1, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};

// System.Action`1<UnityEngine.AsyncOperation>
struct Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB;
// System.Collections.Generic.Dictionary`2<System.Int32,System.Globalization.CultureInfo>
struct Dictionary_2_t9FA6D82CAFC18769F7515BB51D1C56DAE09381C3;
// System.Collections.Generic.Dictionary`2<System.Object,System.Object>
struct Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA;
// System.Collections.Generic.Dictionary`2<System.String,System.Globalization.CultureInfo>
struct Dictionary_2_tE1603CE612C16451D1E56FF4D4859D4FE4087C28;
// System.Collections.Generic.Dictionary`2<System.String,System.Object>
struct Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710;
// Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IDialogResult>
struct FacebookDelegate_1_t2CCD4851FD0B117C9DDD8076206645B712D37148;
// Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IGetTournamentsResult>
struct FacebookDelegate_1_t64F37B84FFAB406D45358FC281716C5AD6B6916A;
// Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IMediaUploadResult>
struct FacebookDelegate_1_t7CA00F6A27B3FE85139590EFEA03B7C7C0D4A66D;
// Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IPayResult>
struct FacebookDelegate_1_t196A2AB9CCB2BC5DCA5BC05F82516E4C3FF9DD4B;
// Facebook.Unity.FacebookDelegate`1<Facebook.Unity.ITournamentScoreResult>
struct FacebookDelegate_1_t474682078C474498C8D4805F13E9077763B39255;
// Facebook.Unity.FacebookDelegate`1<System.Object>
struct FacebookDelegate_1_tC3557293F9F4D8302666EA5C4874312230B814C9;
// System.Collections.Generic.IEqualityComparer`1<System.String>
struct IEqualityComparer_1_tAE94C8F24AD5B94D4EE85CA9FC59E3409D41CAF7;
// System.Collections.Generic.Dictionary`2/KeyCollection<System.String,System.Object>
struct KeyCollection_tE66790F09E854C19C7F612BEAD203AE626E90A36;
// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D;
// System.Collections.Generic.Stack`1<System.String>
struct Stack_1_tD770B7BA3385BBF3A1703E386B6006FF670C5094;
// System.Collections.Generic.Dictionary`2/ValueCollection<System.String,System.Object>
struct ValueCollection_tC9D91E8A3198E40EA339059703AB10DFC9F5CC2E;
// System.Collections.Generic.Dictionary`2/Entry<System.String,System.Object>[]
struct EntryU5BU5D_t233BB24ED01E2D8D65B0651D54B8E3AD125CAF96;
// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
// System.Char[]
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// UnityEngine.GUILayoutOption[]
struct GUILayoutOptionU5BU5D_t24AB80AB9355D784F2C65E12A4D0CC2E0C914CA2;
// System.Int32[]
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
// System.String[]
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
// System.AsyncCallback
struct AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C;
// System.Globalization.Calendar
struct Calendar_t0A117CC7532A54C17188C2EFEA1F79DB20DF3A3B;
// UnityEngine.Networking.CertificateHandler
struct CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804;
// System.Globalization.CompareInfo
struct CompareInfo_t1B1A6AC3486B570C76ABA52149C9BD4CD82F9E57;
// Facebook.Unity.Example.ConsoleBase
struct ConsoleBase_t8E2400E166DEBBECE742939D1714D095BABC074A;
// System.Globalization.CultureData
struct CultureData_tEEFDCF4ECA1BBF6C0C8C94EB3541657245598F9D;
// System.Globalization.CultureInfo
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0;
// System.Globalization.DateTimeFormatInfo
struct DateTimeFormatInfo_t0457520F9FA7B5C8EAAEB3AD50413B6AEEB7458A;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// UnityEngine.Networking.DownloadHandler
struct DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB;
// UnityEngine.GUILayoutOption
struct GUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14;
// UnityEngine.GUIStyle
struct GUIStyle_t20BA2F9F3FE9D13AAA607EEEBE5547835A6F6580;
// System.IAsyncResult
struct IAsyncResult_t7B9B5A0ECB35DCEC31B8A8122C37D687369253B5;
// Facebook.Unity.IDialogResult
struct IDialogResult_tE6B7D2F266B40E2C921776D1B6D9253065C2E62D;
// System.Collections.IDictionary
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
// System.IFormatProvider
struct IFormatProvider_tC202922D43BFF3525109ABF3FB79625F5646AB52;
// Facebook.Unity.IGetTournamentsResult
struct IGetTournamentsResult_t6AD11F29758C4F4D78FFC0FE2D21DD0193C74FAE;
// System.Collections.IList
struct IList_t1C522956D79B7DC92B5B01053DF1AC058C8B598D;
// Facebook.Unity.IMediaUploadResult
struct IMediaUploadResult_tD2E622068BC3194399FE8317B031B5B886F83DEF;
// Facebook.Unity.IPayResult
struct IPayResult_t4E63FC9B25853087E753551D5097B1CC1574894B;
// Facebook.Unity.ITournamentScoreResult
struct ITournamentScoreResult_tB5E0986FA3B48F7181C58C1D2E8B8D45E02ED554;
// Facebook.Unity.Example.MenuBase
struct MenuBase_tDEE50D6BF8974CB32C0B257401501A2675A3C5A8;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// System.Globalization.NumberFormatInfo
struct NumberFormatInfo_t8E26808B202927FEBF9064FCFEEA4D6E076E6472;
// Facebook.Unity.Example.Pay
struct Pay_t148A6DF661BCB2E555B081F94ABD62F3B0E8770E;
// System.String
struct String_t;
// System.Text.StringBuilder
struct StringBuilder_t;
// System.IO.StringReader
struct StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8;
// System.Globalization.TextInfo
struct TextInfo_tD3BAFCFD77418851E7D5CB8D2588F47019E414B4;
// System.IO.TextReader
struct TextReader_tB8D43017CB6BE1633E5A86D64E7757366507C1F7;
// UnityEngine.Texture2D
struct Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4;
// Facebook.Unity.Example.TournamentsMenu
struct TournamentsMenu_tED34C4D1C2BA6799532A19C23CE1F9DB480C9D1D;
// UnityEngine.Networking.UnityWebRequest
struct UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F;
// UnityEngine.Networking.UnityWebRequestAsyncOperation
struct UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C;
// UnityEngine.Networking.UploadHandler
struct UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6;
// Facebook.Unity.Example.UploadToMediaLibrary
struct UploadToMediaLibrary_t7C93E0DCCC3E19053E2626472FF45E6A9036FFCE;
// System.Uri
struct Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E;
// System.UriParser
struct UriParser_t920B0868286118827C08B08A15A9456AF6C19D81;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
// AmplitudeNS.MiniJSON.Json/Parser
struct Parser_t5CE444F52863545C1D883D8E083F9F5C67124951;
// AmplitudeNS.MiniJSON.Json/Serializer
struct Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328;
// System.Uri/UriInfo
struct UriInfo_t5F91F77A93545DDDA6BB24A609BAF5E232CC1A09;

IL2CPP_EXTERN_C RuntimeClass* Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* FB_tD6AF917A642BEC6920761C8E4AD4013414829013_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* FacebookDelegate_1_t196A2AB9CCB2BC5DCA5BC05F82516E4C3FF9DD4B_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* FacebookDelegate_1_t2CCD4851FD0B117C9DDD8076206645B712D37148_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* FacebookDelegate_1_t474682078C474498C8D4805F13E9077763B39255_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* FacebookDelegate_1_t64F37B84FFAB406D45358FC281716C5AD6B6916A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* FacebookDelegate_1_t7CA00F6A27B3FE85139590EFEA03B7C7C0D4A66D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* GUI_tA9CDB3D69DB13D51AD83ABDB587EF95947EC2D2A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerable_t6331596D5DD37C462B1B8D49CF6B319B00AB7131_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IList_t1C522956D79B7DC92B5B01053DF1AC058C8B598D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Parser_t5CE444F52863545C1D883D8E083F9F5C67124951_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* StringBuilder_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* String_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* UInt16_tF4C148C876015C212FD72652D0B6ED8CC247A455_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* UInt32_t1833D51FFA667B18A5AA4B8D34DE284F8495D29B_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* UInt64_t8F12534CC8FC4B5860F2A2CD1EE79D322E7A41AF_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral0FC1909DFE6F4E0D07D99837B774F71D87B79DDD;
IL2CPP_EXTERN_C String_t* _stringLiteral10937E4A05E6010BC7766B4D68EC8E55E43FC0E4;
IL2CPP_EXTERN_C String_t* _stringLiteral1774E11D04A024FCDA7A4FDD51BD9A11A06BB98F;
IL2CPP_EXTERN_C String_t* _stringLiteral225A8B3D65BD41BF9C60DCB20327D49367398223;
IL2CPP_EXTERN_C String_t* _stringLiteral27B1240786558BB01BAF8EA86CF65C4996DEF092;
IL2CPP_EXTERN_C String_t* _stringLiteral41CD180653F0DF900B8BF3930244033A52367BAD;
IL2CPP_EXTERN_C String_t* _stringLiteral4DABC811CE0734D4532568DFC1CDF83347282E52;
IL2CPP_EXTERN_C String_t* _stringLiteral5022C548F0BA2E87BCC042A74207361A4FCEDF6F;
IL2CPP_EXTERN_C String_t* _stringLiteral524D55BD7CC5730C0A3F5924BDA722299B8D0110;
IL2CPP_EXTERN_C String_t* _stringLiteral5962E944D7340CE47999BF097B4AFD70C1501FB9;
IL2CPP_EXTERN_C String_t* _stringLiteral5BEFD8CC60A79699B5BB00E37BAC5B62D371E174;
IL2CPP_EXTERN_C String_t* _stringLiteral5E78319061735653C64261371943DC0AE10A90EC;
IL2CPP_EXTERN_C String_t* _stringLiteral68C98D04ED10D22B52DAA3991A073059D36C5412;
IL2CPP_EXTERN_C String_t* _stringLiteral6ABFA3236CB30A30DF60B11B078D508BF7265D20;
IL2CPP_EXTERN_C String_t* _stringLiteral77D38C0623F92B292B925F6E72CF5CF99A20D4EB;
IL2CPP_EXTERN_C String_t* _stringLiteral785F17F45C331C415D0A7458E6AAC36966399C51;
IL2CPP_EXTERN_C String_t* _stringLiteral7F3238CD8C342B06FB9AB185C610175C84625462;
IL2CPP_EXTERN_C String_t* _stringLiteral848E5ED630B3142F565DD995C6E8D30187ED33CD;
IL2CPP_EXTERN_C String_t* _stringLiteral8A71530774D0DEAE024B0BA9D9C9AF1E058E3D29;
IL2CPP_EXTERN_C String_t* _stringLiteral9A5719B6A850747313EBC7FA287B75B9EAD4024B;
IL2CPP_EXTERN_C String_t* _stringLiteral9CA60916954D1B73462DE743639589D43D096E57;
IL2CPP_EXTERN_C String_t* _stringLiteral9D87FDDD090A10F9879AC40F0B67308856B5FB68;
IL2CPP_EXTERN_C String_t* _stringLiteralA1CB83ED6E8FF560D57C5DE5A8E1CD5AAE2DC401;
IL2CPP_EXTERN_C String_t* _stringLiteralA7C3FCA8C63E127B542B38A5CA5E3FEEDDD1B122;
IL2CPP_EXTERN_C String_t* _stringLiteralAD9219CAA18C36507564E54553997A48F35A0715;
IL2CPP_EXTERN_C String_t* _stringLiteralB13730B3D9E1D51656CEC99169825C5929F0E430;
IL2CPP_EXTERN_C String_t* _stringLiteralB78F235D4291950A7D101307609C259F3E1F033F;
IL2CPP_EXTERN_C String_t* _stringLiteralB7C45DD316C68ABF3429C20058C2981C652192F2;
IL2CPP_EXTERN_C String_t* _stringLiteralC1170DBE5107B0A305F0064F8762968128937776;
IL2CPP_EXTERN_C String_t* _stringLiteralCB1A1E559DB1A4EA52211DCF0A2326F92FFF89D2;
IL2CPP_EXTERN_C String_t* _stringLiteralDA13B1590448DE660CB50261E351C16C14D5CBBB;
IL2CPP_EXTERN_C String_t* _stringLiteralDA666908BB15F4E1D2649752EC5DCBD0D5C64699;
IL2CPP_EXTERN_C String_t* _stringLiteralEA02C841CE2CA1F91740BD3EA483BF6971E48B9B;
IL2CPP_EXTERN_C String_t* _stringLiteralEBC658B067B5C785A3F0BB67D73755F6FEE7F70C;
IL2CPP_EXTERN_C String_t* _stringLiteralF18840F490E42D3CE48CDCBF47229C1C240F8ABE;
IL2CPP_EXTERN_C String_t* _stringLiteralF468E0BCDE9855E7830073A32AF7323CC7E46952;
IL2CPP_EXTERN_C const RuntimeMethod* Array_Empty_TisGUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14_mC7F345AC4C0CA86560FAA00174268F70FBBE577F_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2__ctor_mC4F3DF292BAD88F4BF193C49CD689FAEBC4570A9_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_set_Item_m7CCA97075B48AFB2B97E5A072B94BC7679374341_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* MenuBase_HandleResult_m0CB446ED4B8BDA605B140E61A4A1C6B442765E65_RuntimeMethod_var;
struct CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804_marshaled_com;
struct CultureData_tEEFDCF4ECA1BBF6C0C8C94EB3541657245598F9D_marshaled_com;
struct CultureData_tEEFDCF4ECA1BBF6C0C8C94EB3541657245598F9D_marshaled_pinvoke;
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_com;
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_pinvoke;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB_marshaled_com;
struct UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F_marshaled_com;
struct UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F_marshaled_pinvoke;
struct UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6_marshaled_com;

struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct GUILayoutOptionU5BU5D_t24AB80AB9355D784F2C65E12A4D0CC2E0C914CA2;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Collections.Generic.Dictionary`2<System.String,System.Object>
struct Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710  : public RuntimeObject
{
	// System.Int32[] System.Collections.Generic.Dictionary`2::_buckets
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____buckets_0;
	// System.Collections.Generic.Dictionary`2/Entry<TKey,TValue>[] System.Collections.Generic.Dictionary`2::_entries
	EntryU5BU5D_t233BB24ED01E2D8D65B0651D54B8E3AD125CAF96* ____entries_1;
	// System.Int32 System.Collections.Generic.Dictionary`2::_count
	int32_t ____count_2;
	// System.Int32 System.Collections.Generic.Dictionary`2::_freeList
	int32_t ____freeList_3;
	// System.Int32 System.Collections.Generic.Dictionary`2::_freeCount
	int32_t ____freeCount_4;
	// System.Int32 System.Collections.Generic.Dictionary`2::_version
	int32_t ____version_5;
	// System.Collections.Generic.IEqualityComparer`1<TKey> System.Collections.Generic.Dictionary`2::_comparer
	RuntimeObject* ____comparer_6;
	// System.Collections.Generic.Dictionary`2/KeyCollection<TKey,TValue> System.Collections.Generic.Dictionary`2::_keys
	KeyCollection_tE66790F09E854C19C7F612BEAD203AE626E90A36* ____keys_7;
	// System.Collections.Generic.Dictionary`2/ValueCollection<TKey,TValue> System.Collections.Generic.Dictionary`2::_values
	ValueCollection_tC9D91E8A3198E40EA339059703AB10DFC9F5CC2E* ____values_8;
	// System.Object System.Collections.Generic.Dictionary`2::_syncRoot
	RuntimeObject* ____syncRoot_9;
};

// System.EmptyArray`1<System.Object>
struct EmptyArray_1_tDF0DD7256B115243AA6BD5558417387A734240EE  : public RuntimeObject
{
};

struct EmptyArray_1_tDF0DD7256B115243AA6BD5558417387A734240EE_StaticFields
{
	// T[] System.EmptyArray`1::Value
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___Value_0;
};

// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___s_emptyArray_5;
};
struct Il2CppArrayBounds;

// System.Globalization.CultureInfo
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0  : public RuntimeObject
{
	// System.Boolean System.Globalization.CultureInfo::m_isReadOnly
	bool ___m_isReadOnly_3;
	// System.Int32 System.Globalization.CultureInfo::cultureID
	int32_t ___cultureID_4;
	// System.Int32 System.Globalization.CultureInfo::parent_lcid
	int32_t ___parent_lcid_5;
	// System.Int32 System.Globalization.CultureInfo::datetime_index
	int32_t ___datetime_index_6;
	// System.Int32 System.Globalization.CultureInfo::number_index
	int32_t ___number_index_7;
	// System.Int32 System.Globalization.CultureInfo::default_calendar_type
	int32_t ___default_calendar_type_8;
	// System.Boolean System.Globalization.CultureInfo::m_useUserOverride
	bool ___m_useUserOverride_9;
	// System.Globalization.NumberFormatInfo modreq(System.Runtime.CompilerServices.IsVolatile) System.Globalization.CultureInfo::numInfo
	NumberFormatInfo_t8E26808B202927FEBF9064FCFEEA4D6E076E6472* ___numInfo_10;
	// System.Globalization.DateTimeFormatInfo modreq(System.Runtime.CompilerServices.IsVolatile) System.Globalization.CultureInfo::dateTimeInfo
	DateTimeFormatInfo_t0457520F9FA7B5C8EAAEB3AD50413B6AEEB7458A* ___dateTimeInfo_11;
	// System.Globalization.TextInfo modreq(System.Runtime.CompilerServices.IsVolatile) System.Globalization.CultureInfo::textInfo
	TextInfo_tD3BAFCFD77418851E7D5CB8D2588F47019E414B4* ___textInfo_12;
	// System.String System.Globalization.CultureInfo::m_name
	String_t* ___m_name_13;
	// System.String System.Globalization.CultureInfo::englishname
	String_t* ___englishname_14;
	// System.String System.Globalization.CultureInfo::nativename
	String_t* ___nativename_15;
	// System.String System.Globalization.CultureInfo::iso3lang
	String_t* ___iso3lang_16;
	// System.String System.Globalization.CultureInfo::iso2lang
	String_t* ___iso2lang_17;
	// System.String System.Globalization.CultureInfo::win3lang
	String_t* ___win3lang_18;
	// System.String System.Globalization.CultureInfo::territory
	String_t* ___territory_19;
	// System.String[] System.Globalization.CultureInfo::native_calendar_names
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___native_calendar_names_20;
	// System.Globalization.CompareInfo modreq(System.Runtime.CompilerServices.IsVolatile) System.Globalization.CultureInfo::compareInfo
	CompareInfo_t1B1A6AC3486B570C76ABA52149C9BD4CD82F9E57* ___compareInfo_21;
	// System.Void* System.Globalization.CultureInfo::textinfo_data
	void* ___textinfo_data_22;
	// System.Int32 System.Globalization.CultureInfo::m_dataItem
	int32_t ___m_dataItem_23;
	// System.Globalization.Calendar System.Globalization.CultureInfo::calendar
	Calendar_t0A117CC7532A54C17188C2EFEA1F79DB20DF3A3B* ___calendar_24;
	// System.Globalization.CultureInfo System.Globalization.CultureInfo::parent_culture
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___parent_culture_25;
	// System.Boolean System.Globalization.CultureInfo::constructed
	bool ___constructed_26;
	// System.Byte[] System.Globalization.CultureInfo::cached_serialized_form
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___cached_serialized_form_27;
	// System.Globalization.CultureData System.Globalization.CultureInfo::m_cultureData
	CultureData_tEEFDCF4ECA1BBF6C0C8C94EB3541657245598F9D* ___m_cultureData_28;
	// System.Boolean System.Globalization.CultureInfo::m_isInherited
	bool ___m_isInherited_29;
};

struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_StaticFields
{
	// System.Globalization.CultureInfo modreq(System.Runtime.CompilerServices.IsVolatile) System.Globalization.CultureInfo::invariant_culture_info
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___invariant_culture_info_0;
	// System.Object System.Globalization.CultureInfo::shared_table_lock
	RuntimeObject* ___shared_table_lock_1;
	// System.Globalization.CultureInfo System.Globalization.CultureInfo::default_current_culture
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___default_current_culture_2;
	// System.Globalization.CultureInfo modreq(System.Runtime.CompilerServices.IsVolatile) System.Globalization.CultureInfo::s_DefaultThreadCurrentUICulture
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___s_DefaultThreadCurrentUICulture_34;
	// System.Globalization.CultureInfo modreq(System.Runtime.CompilerServices.IsVolatile) System.Globalization.CultureInfo::s_DefaultThreadCurrentCulture
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___s_DefaultThreadCurrentCulture_35;
	// System.Collections.Generic.Dictionary`2<System.Int32,System.Globalization.CultureInfo> System.Globalization.CultureInfo::shared_by_number
	Dictionary_2_t9FA6D82CAFC18769F7515BB51D1C56DAE09381C3* ___shared_by_number_36;
	// System.Collections.Generic.Dictionary`2<System.String,System.Globalization.CultureInfo> System.Globalization.CultureInfo::shared_by_name
	Dictionary_2_tE1603CE612C16451D1E56FF4D4859D4FE4087C28* ___shared_by_name_37;
	// System.Globalization.CultureInfo System.Globalization.CultureInfo::s_UserPreferredCultureInfoInAppX
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___s_UserPreferredCultureInfoInAppX_38;
	// System.Boolean System.Globalization.CultureInfo::IsTaiwanSku
	bool ___IsTaiwanSku_39;
};
// Native definition for P/Invoke marshalling of System.Globalization.CultureInfo
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_pinvoke
{
	int32_t ___m_isReadOnly_3;
	int32_t ___cultureID_4;
	int32_t ___parent_lcid_5;
	int32_t ___datetime_index_6;
	int32_t ___number_index_7;
	int32_t ___default_calendar_type_8;
	int32_t ___m_useUserOverride_9;
	NumberFormatInfo_t8E26808B202927FEBF9064FCFEEA4D6E076E6472* ___numInfo_10;
	DateTimeFormatInfo_t0457520F9FA7B5C8EAAEB3AD50413B6AEEB7458A* ___dateTimeInfo_11;
	TextInfo_tD3BAFCFD77418851E7D5CB8D2588F47019E414B4* ___textInfo_12;
	char* ___m_name_13;
	char* ___englishname_14;
	char* ___nativename_15;
	char* ___iso3lang_16;
	char* ___iso2lang_17;
	char* ___win3lang_18;
	char* ___territory_19;
	char** ___native_calendar_names_20;
	CompareInfo_t1B1A6AC3486B570C76ABA52149C9BD4CD82F9E57* ___compareInfo_21;
	void* ___textinfo_data_22;
	int32_t ___m_dataItem_23;
	Calendar_t0A117CC7532A54C17188C2EFEA1F79DB20DF3A3B* ___calendar_24;
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_pinvoke* ___parent_culture_25;
	int32_t ___constructed_26;
	Il2CppSafeArray/*NONE*/* ___cached_serialized_form_27;
	CultureData_tEEFDCF4ECA1BBF6C0C8C94EB3541657245598F9D_marshaled_pinvoke* ___m_cultureData_28;
	int32_t ___m_isInherited_29;
};
// Native definition for COM marshalling of System.Globalization.CultureInfo
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_com
{
	int32_t ___m_isReadOnly_3;
	int32_t ___cultureID_4;
	int32_t ___parent_lcid_5;
	int32_t ___datetime_index_6;
	int32_t ___number_index_7;
	int32_t ___default_calendar_type_8;
	int32_t ___m_useUserOverride_9;
	NumberFormatInfo_t8E26808B202927FEBF9064FCFEEA4D6E076E6472* ___numInfo_10;
	DateTimeFormatInfo_t0457520F9FA7B5C8EAAEB3AD50413B6AEEB7458A* ___dateTimeInfo_11;
	TextInfo_tD3BAFCFD77418851E7D5CB8D2588F47019E414B4* ___textInfo_12;
	Il2CppChar* ___m_name_13;
	Il2CppChar* ___englishname_14;
	Il2CppChar* ___nativename_15;
	Il2CppChar* ___iso3lang_16;
	Il2CppChar* ___iso2lang_17;
	Il2CppChar* ___win3lang_18;
	Il2CppChar* ___territory_19;
	Il2CppChar** ___native_calendar_names_20;
	CompareInfo_t1B1A6AC3486B570C76ABA52149C9BD4CD82F9E57* ___compareInfo_21;
	void* ___textinfo_data_22;
	int32_t ___m_dataItem_23;
	Calendar_t0A117CC7532A54C17188C2EFEA1F79DB20DF3A3B* ___calendar_24;
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_com* ___parent_culture_25;
	int32_t ___constructed_26;
	Il2CppSafeArray/*NONE*/* ___cached_serialized_form_27;
	CultureData_tEEFDCF4ECA1BBF6C0C8C94EB3541657245598F9D_marshaled_com* ___m_cultureData_28;
	int32_t ___m_isInherited_29;
};

// UnityEngine.GUILayoutOption
struct GUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14  : public RuntimeObject
{
	// UnityEngine.GUILayoutOption/Type UnityEngine.GUILayoutOption::type
	int32_t ___type_0;
	// System.Object UnityEngine.GUILayoutOption::value
	RuntimeObject* ___value_1;
};

// AmplitudeNS.MiniJSON.Json
struct Json_t15B09338DFB7186D78759C787C6522BADA46CCF7  : public RuntimeObject
{
};

// System.MarshalByRefObject
struct MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE  : public RuntimeObject
{
	// System.Object System.MarshalByRefObject::_identity
	RuntimeObject* ____identity_0;
};
// Native definition for P/Invoke marshalling of System.MarshalByRefObject
struct MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE_marshaled_pinvoke
{
	Il2CppIUnknown* ____identity_0;
};
// Native definition for COM marshalling of System.MarshalByRefObject
struct MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE_marshaled_com
{
	Il2CppIUnknown* ____identity_0;
};

// System.String
struct String_t  : public RuntimeObject
{
	// System.Int32 System.String::_stringLength
	int32_t ____stringLength_4;
	// System.Char System.String::_firstChar
	Il2CppChar ____firstChar_5;
};

struct String_t_StaticFields
{
	// System.String System.String::Empty
	String_t* ___Empty_6;
};

// System.Text.StringBuilder
struct StringBuilder_t  : public RuntimeObject
{
	// System.Char[] System.Text.StringBuilder::m_ChunkChars
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___m_ChunkChars_0;
	// System.Text.StringBuilder System.Text.StringBuilder::m_ChunkPrevious
	StringBuilder_t* ___m_ChunkPrevious_1;
	// System.Int32 System.Text.StringBuilder::m_ChunkLength
	int32_t ___m_ChunkLength_2;
	// System.Int32 System.Text.StringBuilder::m_ChunkOffset
	int32_t ___m_ChunkOffset_3;
	// System.Int32 System.Text.StringBuilder::m_MaxCapacity
	int32_t ___m_MaxCapacity_4;
};

// System.Uri
struct Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E  : public RuntimeObject
{
	// System.String System.Uri::m_String
	String_t* ___m_String_16;
	// System.String System.Uri::m_originalUnicodeString
	String_t* ___m_originalUnicodeString_17;
	// System.UriParser System.Uri::m_Syntax
	UriParser_t920B0868286118827C08B08A15A9456AF6C19D81* ___m_Syntax_18;
	// System.String System.Uri::m_DnsSafeHost
	String_t* ___m_DnsSafeHost_19;
	// System.Uri/Flags System.Uri::m_Flags
	uint64_t ___m_Flags_20;
	// System.Uri/UriInfo System.Uri::m_Info
	UriInfo_t5F91F77A93545DDDA6BB24A609BAF5E232CC1A09* ___m_Info_21;
	// System.Boolean System.Uri::m_iriParsing
	bool ___m_iriParsing_22;
};

struct Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E_StaticFields
{
	// System.String System.Uri::UriSchemeFile
	String_t* ___UriSchemeFile_0;
	// System.String System.Uri::UriSchemeFtp
	String_t* ___UriSchemeFtp_1;
	// System.String System.Uri::UriSchemeGopher
	String_t* ___UriSchemeGopher_2;
	// System.String System.Uri::UriSchemeHttp
	String_t* ___UriSchemeHttp_3;
	// System.String System.Uri::UriSchemeHttps
	String_t* ___UriSchemeHttps_4;
	// System.String System.Uri::UriSchemeWs
	String_t* ___UriSchemeWs_5;
	// System.String System.Uri::UriSchemeWss
	String_t* ___UriSchemeWss_6;
	// System.String System.Uri::UriSchemeMailto
	String_t* ___UriSchemeMailto_7;
	// System.String System.Uri::UriSchemeNews
	String_t* ___UriSchemeNews_8;
	// System.String System.Uri::UriSchemeNntp
	String_t* ___UriSchemeNntp_9;
	// System.String System.Uri::UriSchemeNetTcp
	String_t* ___UriSchemeNetTcp_10;
	// System.String System.Uri::UriSchemeNetPipe
	String_t* ___UriSchemeNetPipe_11;
	// System.String System.Uri::SchemeDelimiter
	String_t* ___SchemeDelimiter_12;
	// System.Boolean modreq(System.Runtime.CompilerServices.IsVolatile) System.Uri::s_ConfigInitialized
	bool ___s_ConfigInitialized_23;
	// System.Boolean modreq(System.Runtime.CompilerServices.IsVolatile) System.Uri::s_ConfigInitializing
	bool ___s_ConfigInitializing_24;
	// System.UriIdnScope modreq(System.Runtime.CompilerServices.IsVolatile) System.Uri::s_IdnScope
	int32_t ___s_IdnScope_25;
	// System.Boolean modreq(System.Runtime.CompilerServices.IsVolatile) System.Uri::s_IriParsing
	bool ___s_IriParsing_26;
	// System.Boolean System.Uri::useDotNetRelativeOrAbsolute
	bool ___useDotNetRelativeOrAbsolute_27;
	// System.Boolean System.Uri::IsWindowsFileSystem
	bool ___IsWindowsFileSystem_29;
	// System.Object System.Uri::s_initLock
	RuntimeObject* ___s_initLock_30;
	// System.Char[] System.Uri::HexLowerChars
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___HexLowerChars_34;
	// System.Char[] System.Uri::_WSchars
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ____WSchars_35;
};

// System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};

// UnityEngine.YieldInstruction
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of UnityEngine.YieldInstruction
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_pinvoke
{
};
// Native definition for COM marshalling of UnityEngine.YieldInstruction
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_com
{
};

// AmplitudeNS.MiniJSON.Json/Parser
struct Parser_t5CE444F52863545C1D883D8E083F9F5C67124951  : public RuntimeObject
{
	// System.IO.StringReader AmplitudeNS.MiniJSON.Json/Parser::json
	StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* ___json_2;
};

// AmplitudeNS.MiniJSON.Json/Serializer
struct Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328  : public RuntimeObject
{
	// System.Text.StringBuilder AmplitudeNS.MiniJSON.Json/Serializer::builder
	StringBuilder_t* ___builder_0;
};

// System.Nullable`1<System.Int32>
struct Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 
{
	// System.Boolean System.Nullable`1::hasValue
	bool ___hasValue_0;
	// T System.Nullable`1::value
	int32_t ___value_1;
};

// System.Nullable`1<System.Single>
struct Nullable_1_t3D746CBB6123D4569FF4DEA60BC4240F32C6FE75 
{
	// System.Boolean System.Nullable`1::hasValue
	bool ___hasValue_0;
	// T System.Nullable`1::value
	float ___value_1;
};

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;
};

struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;
};

// System.Byte
struct Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3 
{
	// System.Byte System.Byte::m_value
	uint8_t ___m_value_0;
};

// System.Char
struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17 
{
	// System.Char System.Char::m_value
	Il2CppChar ___m_value_0;
};

struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17_StaticFields
{
	// System.Byte[] System.Char::s_categoryForLatin1
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___s_categoryForLatin1_3;
};

// System.DateTime
struct DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D 
{
	// System.UInt64 System.DateTime::_dateData
	uint64_t ____dateData_46;
};

struct DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_StaticFields
{
	// System.Int32[] System.DateTime::s_daysToMonth365
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ___s_daysToMonth365_30;
	// System.Int32[] System.DateTime::s_daysToMonth366
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ___s_daysToMonth366_31;
	// System.DateTime System.DateTime::MinValue
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___MinValue_32;
	// System.DateTime System.DateTime::MaxValue
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___MaxValue_33;
	// System.DateTime System.DateTime::UnixEpoch
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___UnixEpoch_34;
};

// System.Decimal
struct Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F 
{
	union
	{
		#pragma pack(push, tp, 1)
		struct
		{
			// System.Int32 System.Decimal::flags
			int32_t ___flags_8;
		};
		#pragma pack(pop, tp)
		struct
		{
			int32_t ___flags_8_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___hi_9_OffsetPadding[4];
			// System.Int32 System.Decimal::hi
			int32_t ___hi_9;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___hi_9_OffsetPadding_forAlignmentOnly[4];
			int32_t ___hi_9_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___lo_10_OffsetPadding[8];
			// System.Int32 System.Decimal::lo
			int32_t ___lo_10;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___lo_10_OffsetPadding_forAlignmentOnly[8];
			int32_t ___lo_10_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___mid_11_OffsetPadding[12];
			// System.Int32 System.Decimal::mid
			int32_t ___mid_11;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___mid_11_OffsetPadding_forAlignmentOnly[12];
			int32_t ___mid_11_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___ulomidLE_12_OffsetPadding[8];
			// System.UInt64 System.Decimal::ulomidLE
			uint64_t ___ulomidLE_12;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___ulomidLE_12_OffsetPadding_forAlignmentOnly[8];
			uint64_t ___ulomidLE_12_forAlignmentOnly;
		};
	};
};

struct Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_StaticFields
{
	// System.Decimal System.Decimal::Zero
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___Zero_3;
	// System.Decimal System.Decimal::One
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___One_4;
	// System.Decimal System.Decimal::MinusOne
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___MinusOne_5;
	// System.Decimal System.Decimal::MaxValue
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___MaxValue_6;
	// System.Decimal System.Decimal::MinValue
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___MinValue_7;
};

// System.Double
struct Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F 
{
	// System.Double System.Double::m_value
	double ___m_value_0;
};

// System.Int16
struct Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175 
{
	// System.Int16 System.Int16::m_value
	int16_t ___m_value_0;
};

// System.Int32
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	// System.Int32 System.Int32::m_value
	int32_t ___m_value_0;
};

// System.Int64
struct Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3 
{
	// System.Int64 System.Int64::m_value
	int64_t ___m_value_0;
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
};

struct IntPtr_t_StaticFields
{
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;
};

// System.SByte
struct SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5 
{
	// System.SByte System.SByte::m_value
	int8_t ___m_value_0;
};

// System.Single
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	// System.Single System.Single::m_value
	float ___m_value_0;
};

// System.IO.TextReader
struct TextReader_tB8D43017CB6BE1633E5A86D64E7757366507C1F7  : public MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE
{
};

struct TextReader_tB8D43017CB6BE1633E5A86D64E7757366507C1F7_StaticFields
{
	// System.IO.TextReader System.IO.TextReader::Null
	TextReader_tB8D43017CB6BE1633E5A86D64E7757366507C1F7* ___Null_1;
};

// System.UInt16
struct UInt16_tF4C148C876015C212FD72652D0B6ED8CC247A455 
{
	// System.UInt16 System.UInt16::m_value
	uint16_t ___m_value_0;
};

// System.UInt32
struct UInt32_t1833D51FFA667B18A5AA4B8D34DE284F8495D29B 
{
	// System.UInt32 System.UInt32::m_value
	uint32_t ___m_value_0;
};

// System.UInt64
struct UInt64_t8F12534CC8FC4B5860F2A2CD1EE79D322E7A41AF 
{
	// System.UInt64 System.UInt64::m_value
	uint64_t ___m_value_0;
};

// UnityEngine.Vector2
struct Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 
{
	// System.Single UnityEngine.Vector2::x
	float ___x_0;
	// System.Single UnityEngine.Vector2::y
	float ___y_1;
};

struct Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7_StaticFields
{
	// UnityEngine.Vector2 UnityEngine.Vector2::zeroVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___zeroVector_2;
	// UnityEngine.Vector2 UnityEngine.Vector2::oneVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___oneVector_3;
	// UnityEngine.Vector2 UnityEngine.Vector2::upVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___upVector_4;
	// UnityEngine.Vector2 UnityEngine.Vector2::downVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___downVector_5;
	// UnityEngine.Vector2 UnityEngine.Vector2::leftVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___leftVector_6;
	// UnityEngine.Vector2 UnityEngine.Vector2::rightVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___rightVector_7;
	// UnityEngine.Vector2 UnityEngine.Vector2::positiveInfinityVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___positiveInfinityVector_8;
	// UnityEngine.Vector2 UnityEngine.Vector2::negativeInfinityVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___negativeInfinityVector_9;
};

// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=32
struct __StaticArrayInitTypeSizeU3D32_tC3894D25C1E879699FE1C9BAB1BBF2787B405069 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D32_tC3894D25C1E879699FE1C9BAB1BBF2787B405069__padding[32];
	};
};

// <PrivateImplementationDetails>
struct U3CPrivateImplementationDetailsU3E_t0F5473E849A5A5185A9F4C5246F0C32816C49FCA  : public RuntimeObject
{
};

struct U3CPrivateImplementationDetailsU3E_t0F5473E849A5A5185A9F4C5246F0C32816C49FCA_StaticFields
{
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=32 <PrivateImplementationDetails>::067EAF3F727B985767F95E6014895724A51BEC050B158CBF78429570BF08F888
	__StaticArrayInitTypeSizeU3D32_tC3894D25C1E879699FE1C9BAB1BBF2787B405069 ___067EAF3F727B985767F95E6014895724A51BEC050B158CBF78429570BF08F888_0;
};

// UnityEngine.AsyncOperation
struct AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C  : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D
{
	// System.IntPtr UnityEngine.AsyncOperation::m_Ptr
	intptr_t ___m_Ptr_0;
	// System.Action`1<UnityEngine.AsyncOperation> UnityEngine.AsyncOperation::m_completeCallback
	Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB* ___m_completeCallback_1;
};
// Native definition for P/Invoke marshalling of UnityEngine.AsyncOperation
struct AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_pinvoke : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_pinvoke
{
	intptr_t ___m_Ptr_0;
	Il2CppMethodPointer ___m_completeCallback_1;
};
// Native definition for COM marshalling of UnityEngine.AsyncOperation
struct AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_com : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_com
{
	intptr_t ___m_Ptr_0;
	Il2CppMethodPointer ___m_completeCallback_1;
};

// UnityEngine.Networking.CertificateHandler
struct CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Networking.CertificateHandler::m_Ptr
	intptr_t ___m_Ptr_0;
};
// Native definition for P/Invoke marshalling of UnityEngine.Networking.CertificateHandler
struct CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804_marshaled_pinvoke
{
	intptr_t ___m_Ptr_0;
};
// Native definition for COM marshalling of UnityEngine.Networking.CertificateHandler
struct CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804_marshaled_com
{
	intptr_t ___m_Ptr_0;
};

// System.Delegate
struct Delegate_t  : public RuntimeObject
{
	// System.IntPtr System.Delegate::method_ptr
	Il2CppMethodPointer ___method_ptr_0;
	// System.IntPtr System.Delegate::invoke_impl
	intptr_t ___invoke_impl_1;
	// System.Object System.Delegate::m_target
	RuntimeObject* ___m_target_2;
	// System.IntPtr System.Delegate::method
	intptr_t ___method_3;
	// System.IntPtr System.Delegate::delegate_trampoline
	intptr_t ___delegate_trampoline_4;
	// System.IntPtr System.Delegate::extra_arg
	intptr_t ___extra_arg_5;
	// System.IntPtr System.Delegate::method_code
	intptr_t ___method_code_6;
	// System.IntPtr System.Delegate::interp_method
	intptr_t ___interp_method_7;
	// System.IntPtr System.Delegate::interp_invoke_impl
	intptr_t ___interp_invoke_impl_8;
	// System.Reflection.MethodInfo System.Delegate::method_info
	MethodInfo_t* ___method_info_9;
	// System.Reflection.MethodInfo System.Delegate::original_method_info
	MethodInfo_t* ___original_method_info_10;
	// System.DelegateData System.Delegate::data
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	// System.Boolean System.Delegate::method_is_virtual
	bool ___method_is_virtual_12;
};
// Native definition for P/Invoke marshalling of System.Delegate
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};
// Native definition for COM marshalling of System.Delegate
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};

// UnityEngine.Networking.DownloadHandler
struct DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Networking.DownloadHandler::m_Ptr
	intptr_t ___m_Ptr_0;
};
// Native definition for P/Invoke marshalling of UnityEngine.Networking.DownloadHandler
struct DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB_marshaled_pinvoke
{
	intptr_t ___m_Ptr_0;
};
// Native definition for COM marshalling of UnityEngine.Networking.DownloadHandler
struct DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB_marshaled_com
{
	intptr_t ___m_Ptr_0;
};

// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Object::m_CachedPtr
	intptr_t ___m_CachedPtr_0;
};

struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	// System.Int32 UnityEngine.Object::OffsetOfInstanceIDInCPlusPlusObject
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject_1;
};
// Native definition for P/Invoke marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr_0;
};
// Native definition for COM marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr_0;
};

// System.IO.StringReader
struct StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8  : public TextReader_tB8D43017CB6BE1633E5A86D64E7757366507C1F7
{
	// System.String System.IO.StringReader::_s
	String_t* ____s_2;
	// System.Int32 System.IO.StringReader::_pos
	int32_t ____pos_3;
	// System.Int32 System.IO.StringReader::_length
	int32_t ____length_4;
};

// UnityEngine.Networking.UploadHandler
struct UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Networking.UploadHandler::m_Ptr
	intptr_t ___m_Ptr_0;
};
// Native definition for P/Invoke marshalling of UnityEngine.Networking.UploadHandler
struct UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6_marshaled_pinvoke
{
	intptr_t ___m_Ptr_0;
};
// Native definition for COM marshalling of UnityEngine.Networking.UploadHandler
struct UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6_marshaled_com
{
	intptr_t ___m_Ptr_0;
};

// UnityEngine.Component
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// System.MulticastDelegate
struct MulticastDelegate_t  : public Delegate_t
{
	// System.Delegate[] System.MulticastDelegate::delegates
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates_13;
};
// Native definition for P/Invoke marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates_13;
};
// Native definition for COM marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates_13;
};

// UnityEngine.Networking.UnityWebRequest
struct UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Networking.UnityWebRequest::m_Ptr
	intptr_t ___m_Ptr_0;
	// UnityEngine.Networking.DownloadHandler UnityEngine.Networking.UnityWebRequest::m_DownloadHandler
	DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB* ___m_DownloadHandler_1;
	// UnityEngine.Networking.UploadHandler UnityEngine.Networking.UnityWebRequest::m_UploadHandler
	UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6* ___m_UploadHandler_2;
	// UnityEngine.Networking.CertificateHandler UnityEngine.Networking.UnityWebRequest::m_CertificateHandler
	CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804* ___m_CertificateHandler_3;
	// System.Uri UnityEngine.Networking.UnityWebRequest::m_Uri
	Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E* ___m_Uri_4;
	// System.Boolean UnityEngine.Networking.UnityWebRequest::<disposeCertificateHandlerOnDispose>k__BackingField
	bool ___U3CdisposeCertificateHandlerOnDisposeU3Ek__BackingField_5;
	// System.Boolean UnityEngine.Networking.UnityWebRequest::<disposeDownloadHandlerOnDispose>k__BackingField
	bool ___U3CdisposeDownloadHandlerOnDisposeU3Ek__BackingField_6;
	// System.Boolean UnityEngine.Networking.UnityWebRequest::<disposeUploadHandlerOnDispose>k__BackingField
	bool ___U3CdisposeUploadHandlerOnDisposeU3Ek__BackingField_7;
};
// Native definition for P/Invoke marshalling of UnityEngine.Networking.UnityWebRequest
struct UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F_marshaled_pinvoke
{
	intptr_t ___m_Ptr_0;
	DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB_marshaled_pinvoke ___m_DownloadHandler_1;
	UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6_marshaled_pinvoke ___m_UploadHandler_2;
	CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804_marshaled_pinvoke ___m_CertificateHandler_3;
	Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E* ___m_Uri_4;
	int32_t ___U3CdisposeCertificateHandlerOnDisposeU3Ek__BackingField_5;
	int32_t ___U3CdisposeDownloadHandlerOnDisposeU3Ek__BackingField_6;
	int32_t ___U3CdisposeUploadHandlerOnDisposeU3Ek__BackingField_7;
};
// Native definition for COM marshalling of UnityEngine.Networking.UnityWebRequest
struct UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F_marshaled_com
{
	intptr_t ___m_Ptr_0;
	DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB_marshaled_com* ___m_DownloadHandler_1;
	UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6_marshaled_com* ___m_UploadHandler_2;
	CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804_marshaled_com* ___m_CertificateHandler_3;
	Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E* ___m_Uri_4;
	int32_t ___U3CdisposeCertificateHandlerOnDisposeU3Ek__BackingField_5;
	int32_t ___U3CdisposeDownloadHandlerOnDisposeU3Ek__BackingField_6;
	int32_t ___U3CdisposeUploadHandlerOnDisposeU3Ek__BackingField_7;
};

// UnityEngine.Networking.UnityWebRequestAsyncOperation
struct UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C  : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C
{
	// UnityEngine.Networking.UnityWebRequest UnityEngine.Networking.UnityWebRequestAsyncOperation::<webRequest>k__BackingField
	UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* ___U3CwebRequestU3Ek__BackingField_2;
};
// Native definition for P/Invoke marshalling of UnityEngine.Networking.UnityWebRequestAsyncOperation
struct UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C_marshaled_pinvoke : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_pinvoke
{
	UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F_marshaled_pinvoke* ___U3CwebRequestU3Ek__BackingField_2;
};
// Native definition for COM marshalling of UnityEngine.Networking.UnityWebRequestAsyncOperation
struct UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C_marshaled_com : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_com
{
	UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F_marshaled_com* ___U3CwebRequestU3Ek__BackingField_2;
};

// Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IDialogResult>
struct FacebookDelegate_1_t2CCD4851FD0B117C9DDD8076206645B712D37148  : public MulticastDelegate_t
{
};

// Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IGetTournamentsResult>
struct FacebookDelegate_1_t64F37B84FFAB406D45358FC281716C5AD6B6916A  : public MulticastDelegate_t
{
};

// Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IMediaUploadResult>
struct FacebookDelegate_1_t7CA00F6A27B3FE85139590EFEA03B7C7C0D4A66D  : public MulticastDelegate_t
{
};

// Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IPayResult>
struct FacebookDelegate_1_t196A2AB9CCB2BC5DCA5BC05F82516E4C3FF9DD4B  : public MulticastDelegate_t
{
};

// Facebook.Unity.FacebookDelegate`1<Facebook.Unity.ITournamentScoreResult>
struct FacebookDelegate_1_t474682078C474498C8D4805F13E9077763B39255  : public MulticastDelegate_t
{
};

// UnityEngine.Behaviour
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};

// UnityEngine.MonoBehaviour
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
};

// Facebook.Unity.Example.ConsoleBase
struct ConsoleBase_t8E2400E166DEBBECE742939D1714D095BABC074A  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// System.String Facebook.Unity.Example.ConsoleBase::status
	String_t* ___status_6;
	// System.String Facebook.Unity.Example.ConsoleBase::lastResponse
	String_t* ___lastResponse_7;
	// UnityEngine.Vector2 Facebook.Unity.Example.ConsoleBase::scrollPosition
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___scrollPosition_8;
	// System.Nullable`1<System.Single> Facebook.Unity.Example.ConsoleBase::scaleFactor
	Nullable_1_t3D746CBB6123D4569FF4DEA60BC4240F32C6FE75 ___scaleFactor_9;
	// UnityEngine.GUIStyle Facebook.Unity.Example.ConsoleBase::textStyle
	GUIStyle_t20BA2F9F3FE9D13AAA607EEEBE5547835A6F6580* ___textStyle_10;
	// UnityEngine.GUIStyle Facebook.Unity.Example.ConsoleBase::buttonStyle
	GUIStyle_t20BA2F9F3FE9D13AAA607EEEBE5547835A6F6580* ___buttonStyle_11;
	// UnityEngine.GUIStyle Facebook.Unity.Example.ConsoleBase::textInputStyle
	GUIStyle_t20BA2F9F3FE9D13AAA607EEEBE5547835A6F6580* ___textInputStyle_12;
	// UnityEngine.GUIStyle Facebook.Unity.Example.ConsoleBase::labelStyle
	GUIStyle_t20BA2F9F3FE9D13AAA607EEEBE5547835A6F6580* ___labelStyle_13;
	// UnityEngine.Texture2D Facebook.Unity.Example.ConsoleBase::<LastResponseTexture>k__BackingField
	Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* ___U3CLastResponseTextureU3Ek__BackingField_14;
};

struct ConsoleBase_t8E2400E166DEBBECE742939D1714D095BABC074A_StaticFields
{
	// System.Collections.Generic.Stack`1<System.String> Facebook.Unity.Example.ConsoleBase::menuStack
	Stack_1_tD770B7BA3385BBF3A1703E386B6006FF670C5094* ___menuStack_5;
};

// Facebook.Unity.Example.MenuBase
struct MenuBase_tDEE50D6BF8974CB32C0B257401501A2675A3C5A8  : public ConsoleBase_t8E2400E166DEBBECE742939D1714D095BABC074A
{
};

struct MenuBase_tDEE50D6BF8974CB32C0B257401501A2675A3C5A8_StaticFields
{
	// Facebook.Unity.ShareDialogMode Facebook.Unity.Example.MenuBase::shareDialogMode
	int32_t ___shareDialogMode_15;
};

// Facebook.Unity.Example.Pay
struct Pay_t148A6DF661BCB2E555B081F94ABD62F3B0E8770E  : public MenuBase_tDEE50D6BF8974CB32C0B257401501A2675A3C5A8
{
	// System.String Facebook.Unity.Example.Pay::payProduct
	String_t* ___payProduct_16;
};

// Facebook.Unity.Example.TournamentsMenu
struct TournamentsMenu_tED34C4D1C2BA6799532A19C23CE1F9DB480C9D1D  : public MenuBase_tDEE50D6BF8974CB32C0B257401501A2675A3C5A8
{
	// System.String Facebook.Unity.Example.TournamentsMenu::score
	String_t* ___score_16;
	// System.String Facebook.Unity.Example.TournamentsMenu::tournamentID
	String_t* ___tournamentID_17;
};

// Facebook.Unity.Example.UploadToMediaLibrary
struct UploadToMediaLibrary_t7C93E0DCCC3E19053E2626472FF45E6A9036FFCE  : public MenuBase_tDEE50D6BF8974CB32C0B257401501A2675A3C5A8
{
	// System.Boolean Facebook.Unity.Example.UploadToMediaLibrary::imageShouldLaunchMediaDialog
	bool ___imageShouldLaunchMediaDialog_16;
	// System.String Facebook.Unity.Example.UploadToMediaLibrary::imageCaption
	String_t* ___imageCaption_17;
	// System.String Facebook.Unity.Example.UploadToMediaLibrary::imageFile
	String_t* ___imageFile_18;
	// System.Boolean Facebook.Unity.Example.UploadToMediaLibrary::videoShouldLaunchMediaDialog
	bool ___videoShouldLaunchMediaDialog_19;
	// System.String Facebook.Unity.Example.UploadToMediaLibrary::videoCaption
	String_t* ___videoCaption_20;
	// System.String Facebook.Unity.Example.UploadToMediaLibrary::videoFile
	String_t* ___videoFile_21;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// UnityEngine.GUILayoutOption[]
struct GUILayoutOptionU5BU5D_t24AB80AB9355D784F2C65E12A4D0CC2E0C914CA2  : public RuntimeArray
{
	ALIGN_FIELD (8) GUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14* m_Items[1];

	inline GUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline GUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, GUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline GUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline GUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, GUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031  : public RuntimeArray
{
	ALIGN_FIELD (8) uint8_t m_Items[1];

	inline uint8_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline uint8_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, uint8_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline uint8_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline uint8_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, uint8_t value)
	{
		m_Items[index] = value;
	}
};
// System.Char[]
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB  : public RuntimeArray
{
	ALIGN_FIELD (8) Il2CppChar m_Items[1];

	inline Il2CppChar GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Il2CppChar* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Il2CppChar value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline Il2CppChar GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Il2CppChar* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Il2CppChar value)
	{
		m_Items[index] = value;
	}
};
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918  : public RuntimeArray
{
	ALIGN_FIELD (8) RuntimeObject* m_Items[1];

	inline RuntimeObject* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, RuntimeObject* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline RuntimeObject* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, RuntimeObject* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};


// System.Void Facebook.Unity.FacebookDelegate`1<System.Object>::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FacebookDelegate_1__ctor_m44F1504FE737AA983D8B198477F3B73757EB69B5_gshared (FacebookDelegate_1_tC3557293F9F4D8302666EA5C4874312230B814C9* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method) ;
// T[] System.Array::Empty<System.Object>()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_gshared_inline (const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Object,System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2__ctor_m5B32FBC624618211EB461D59CFBB10E987FD1329_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Object,System.Object>::set_Item(TKey,TValue)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2_set_Item_m1A840355E8EDAECEA9D0C6F5E51B248FAA449CBD_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, RuntimeObject* ___key0, RuntimeObject* ___value1, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Object>::Add(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___item0, const RuntimeMethod* method) ;

// System.Void Facebook.Unity.Example.ConsoleBase::LabelAndTextField(System.String,System.String&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConsoleBase_LabelAndTextField_m72E1BBF75934CE747195CB87737A2D7EFC503A8A (ConsoleBase_t8E2400E166DEBBECE742939D1714D095BABC074A* __this, String_t* ___label0, String_t** ___text1, const RuntimeMethod* method) ;
// System.Boolean Facebook.Unity.Example.ConsoleBase::Button(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ConsoleBase_Button_m8787498ECF036B9B3B5EB6B2FF49AFA154282D10 (ConsoleBase_t8E2400E166DEBBECE742939D1714D095BABC074A* __this, String_t* ___label0, const RuntimeMethod* method) ;
// System.Void Facebook.Unity.Example.Pay::CallFBPay()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Pay_CallFBPay_m6C1243E4AA248F715366AA5E8996D25431AA2A58 (Pay_t148A6DF661BCB2E555B081F94ABD62F3B0E8770E* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.GUILayout::Space(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GUILayout_Space_m9254FBF173F9260DDB6C83C0066447FC9D9CA597 (float ___pixels0, const RuntimeMethod* method) ;
// System.Void Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IPayResult>::.ctor(System.Object,System.IntPtr)
inline void FacebookDelegate_1__ctor_mC91675ED1A36D58BAA4B5C50CA96AB135B6CB444 (FacebookDelegate_1_t196A2AB9CCB2BC5DCA5BC05F82516E4C3FF9DD4B* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method)
{
	((  void (*) (FacebookDelegate_1_t196A2AB9CCB2BC5DCA5BC05F82516E4C3FF9DD4B*, RuntimeObject*, intptr_t, const RuntimeMethod*))FacebookDelegate_1__ctor_m44F1504FE737AA983D8B198477F3B73757EB69B5_gshared)(__this, ___object0, ___method1, method);
}
// System.Void Facebook.Unity.FB/Canvas::Pay(System.String,System.String,System.Int32,System.Nullable`1<System.Int32>,System.Nullable`1<System.Int32>,System.String,System.String,System.String,Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IPayResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Canvas_Pay_m0C127FE00EDA7742A93998ACAB67A65F3103539A (String_t* ___product0, String_t* ___action1, int32_t ___quantity2, Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 ___quantityMin3, Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 ___quantityMax4, String_t* ___requestId5, String_t* ___pricepointId6, String_t* ___testCurrency7, FacebookDelegate_1_t196A2AB9CCB2BC5DCA5BC05F82516E4C3FF9DD4B* ___callback8, const RuntimeMethod* method) ;
// System.Void Facebook.Unity.Example.MenuBase::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MenuBase__ctor_mD1A153F6EBCE35B5990669A20787B391A5DEC0E2 (MenuBase_tDEE50D6BF8974CB32C0B257401501A2675A3C5A8* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.GUI::get_enabled()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool GUI_get_enabled_m336E115A84DBD8D18A925D0755B51746B98B516D (const RuntimeMethod* method) ;
// System.Boolean Facebook.Unity.FB::get_IsLoggedIn()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool FB_get_IsLoggedIn_m0E22CD5EE863598BFBBB29D65F36245A15B9302A (const RuntimeMethod* method) ;
// System.Void UnityEngine.GUI::set_enabled(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GUI_set_enabled_mF2F99A6870ACAFAEFB5E8FF1B69C684951D390C9 (bool ___value0, const RuntimeMethod* method) ;
// System.Void Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IGetTournamentsResult>::.ctor(System.Object,System.IntPtr)
inline void FacebookDelegate_1__ctor_mC41A149F693D6BF00F1D38ED5EC2368B2B5A1328 (FacebookDelegate_1_t64F37B84FFAB406D45358FC281716C5AD6B6916A* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method)
{
	((  void (*) (FacebookDelegate_1_t64F37B84FFAB406D45358FC281716C5AD6B6916A*, RuntimeObject*, intptr_t, const RuntimeMethod*))FacebookDelegate_1__ctor_m44F1504FE737AA983D8B198477F3B73757EB69B5_gshared)(__this, ___object0, ___method1, method);
}
// System.Void Facebook.Unity.FB/Mobile::GetTournaments(Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IGetTournamentsResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Mobile_GetTournaments_mBA4EED8CDA8C21A64C1A201275C3C34F71F8F12C (FacebookDelegate_1_t64F37B84FFAB406D45358FC281716C5AD6B6916A* ___callback0, const RuntimeMethod* method) ;
// System.Int32 System.Int32::Parse(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Int32_Parse_m273CA1A9C7717C99641291A95C543711C0202AF0 (String_t* ___s0, const RuntimeMethod* method) ;
// System.Void Facebook.Unity.FacebookDelegate`1<Facebook.Unity.ITournamentScoreResult>::.ctor(System.Object,System.IntPtr)
inline void FacebookDelegate_1__ctor_mE1DA08C38DC193E943FBB7C052938E717EABBD4A (FacebookDelegate_1_t474682078C474498C8D4805F13E9077763B39255* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method)
{
	((  void (*) (FacebookDelegate_1_t474682078C474498C8D4805F13E9077763B39255*, RuntimeObject*, intptr_t, const RuntimeMethod*))FacebookDelegate_1__ctor_m44F1504FE737AA983D8B198477F3B73757EB69B5_gshared)(__this, ___object0, ___method1, method);
}
// System.Void Facebook.Unity.FB/Mobile::UpdateTournament(System.String,System.Int32,Facebook.Unity.FacebookDelegate`1<Facebook.Unity.ITournamentScoreResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Mobile_UpdateTournament_mA3EB37B93D1C30F8F320EBFE704AEE759A96194A (String_t* ___tournamentID0, int32_t ___score1, FacebookDelegate_1_t474682078C474498C8D4805F13E9077763B39255* ___callback2, const RuntimeMethod* method) ;
// System.Void Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IDialogResult>::.ctor(System.Object,System.IntPtr)
inline void FacebookDelegate_1__ctor_mB15E6C38086F8341B29BE751BD0F95FA93E57793 (FacebookDelegate_1_t2CCD4851FD0B117C9DDD8076206645B712D37148* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method)
{
	((  void (*) (FacebookDelegate_1_t2CCD4851FD0B117C9DDD8076206645B712D37148*, RuntimeObject*, intptr_t, const RuntimeMethod*))FacebookDelegate_1__ctor_m44F1504FE737AA983D8B198477F3B73757EB69B5_gshared)(__this, ___object0, ___method1, method);
}
// System.Void Facebook.Unity.FB/Mobile::UpdateAndShareTournament(System.String,System.Int32,Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IDialogResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Mobile_UpdateAndShareTournament_m49A8A9E8EA3501EA782E3ADBE9789A180663D5E3 (String_t* ___tournamentID0, int32_t ___score1, FacebookDelegate_1_t2CCD4851FD0B117C9DDD8076206645B712D37148* ___callback2, const RuntimeMethod* method) ;
// System.DateTime System.DateTime::get_UtcNow()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D DateTime_get_UtcNow_m06B6E9995FE16846A0F71EC9DB23E90BE2C5F9FA (const RuntimeMethod* method) ;
// System.DateTime System.DateTime::AddHours(System.Double)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D DateTime_AddHours_m99C41C078F2F480BF9965F8A4BAB8C8B75C39C02 (DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D* __this, double ___value0, const RuntimeMethod* method) ;
// System.Void Facebook.Unity.FB/Mobile::CreateAndShareTournament(System.Int32,System.String,TournamentSortOrder,TournamentScoreFormat,System.DateTime,System.String,Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IDialogResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Mobile_CreateAndShareTournament_m41E95D4C33D17E97E0D70284DBAC5E1B2736930F (int32_t ___initialScore0, String_t* ___title1, int32_t ___sortOrder2, int32_t ___scoreFormat3, DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___endTime4, String_t* ___payload5, FacebookDelegate_1_t2CCD4851FD0B117C9DDD8076206645B712D37148* ___callback6, const RuntimeMethod* method) ;
// System.String System.Boolean::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Boolean_ToString_m6646C8026B1DF381A1EE8CD13549175E9703CC63 (bool* __this, const RuntimeMethod* method) ;
// System.String System.String::Concat(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m9E3155FB84015C823606188F53B47CB44C444991 (String_t* ___str00, String_t* ___str11, const RuntimeMethod* method) ;
// System.String Facebook.Unity.Example.UploadToMediaLibrary::GetPath(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* UploadToMediaLibrary_GetPath_mDBF5060B2EC32877F865F24B6D97C420B7610760 (UploadToMediaLibrary_t7C93E0DCCC3E19053E2626472FF45E6A9036FFCE* __this, String_t* ___filename0, const RuntimeMethod* method) ;
// System.Boolean System.IO.File::Exists(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool File_Exists_m95E329ABBE3EAD6750FE1989BBA6884457136D4A (String_t* ___path0, const RuntimeMethod* method) ;
// System.Void System.Uri::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Uri__ctor_m6CA436E6AD2768A121FA851CBEEFA3623E849D3A (Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E* __this, String_t* ___uriString0, const RuntimeMethod* method) ;
// System.Void Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IMediaUploadResult>::.ctor(System.Object,System.IntPtr)
inline void FacebookDelegate_1__ctor_m8A5848CA226A79CA3779B0A21AC0E1DDEF7908F7 (FacebookDelegate_1_t7CA00F6A27B3FE85139590EFEA03B7C7C0D4A66D* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method)
{
	((  void (*) (FacebookDelegate_1_t7CA00F6A27B3FE85139590EFEA03B7C7C0D4A66D*, RuntimeObject*, intptr_t, const RuntimeMethod*))FacebookDelegate_1__ctor_m44F1504FE737AA983D8B198477F3B73757EB69B5_gshared)(__this, ___object0, ___method1, method);
}
// System.Void Facebook.Unity.FBGamingServices::UploadImageToMediaLibrary(System.String,System.Uri,System.Boolean,Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IMediaUploadResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FBGamingServices_UploadImageToMediaLibrary_m834FB0476EA0E1777F7509239AF063283A9A87BF (String_t* ___caption0, Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E* ___imageUri1, bool ___shouldLaunchMediaDialog2, FacebookDelegate_1_t7CA00F6A27B3FE85139590EFEA03B7C7C0D4A66D* ___callback3, const RuntimeMethod* method) ;
// T[] System.Array::Empty<UnityEngine.GUILayoutOption>()
inline GUILayoutOptionU5BU5D_t24AB80AB9355D784F2C65E12A4D0CC2E0C914CA2* Array_Empty_TisGUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14_mC7F345AC4C0CA86560FAA00174268F70FBBE577F_inline (const RuntimeMethod* method)
{
	return ((  GUILayoutOptionU5BU5D_t24AB80AB9355D784F2C65E12A4D0CC2E0C914CA2* (*) (const RuntimeMethod*))Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_gshared_inline)(method);
}
// System.Void UnityEngine.GUILayout::Label(System.String,UnityEngine.GUILayoutOption[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GUILayout_Label_m1709C16A433383CCFC1FEA0E585E14CBD78CD94B (String_t* ___text0, GUILayoutOptionU5BU5D_t24AB80AB9355D784F2C65E12A4D0CC2E0C914CA2* ___options1, const RuntimeMethod* method) ;
// System.Void Facebook.Unity.FBGamingServices::UploadVideoToMediaLibrary(System.String,System.Uri,System.Boolean,Facebook.Unity.FacebookDelegate`1<Facebook.Unity.IMediaUploadResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FBGamingServices_UploadVideoToMediaLibrary_m1ADD423D1D4A24824D688D23211014B7F121F066 (String_t* ___caption0, Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E* ___videoUri1, bool ___shouldLaunchMediaDialog2, FacebookDelegate_1_t7CA00F6A27B3FE85139590EFEA03B7C7C0D4A66D* ___callback3, const RuntimeMethod* method) ;
// System.String UnityEngine.Application::get_streamingAssetsPath()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Application_get_streamingAssetsPath_mB904BCD9A7A4F18A52C175DE4A81F5DC3010CDB5 (const RuntimeMethod* method) ;
// System.String System.IO.Path::Combine(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Path_Combine_m1ADAC05CDA2D1D61B172DF65A81E86592696BEAE (String_t* ___path10, String_t* ___path21, const RuntimeMethod* method) ;
// UnityEngine.Networking.UnityWebRequest UnityEngine.Networking.UnityWebRequest::Get(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* UnityWebRequest_Get_m1A332EE069BB5052368307F254A5A7627BB5FD86 (String_t* ___uri0, const RuntimeMethod* method) ;
// UnityEngine.Networking.UnityWebRequestAsyncOperation UnityEngine.Networking.UnityWebRequest::SendWebRequest()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C* UnityWebRequest_SendWebRequest_mA3CD13983BAA5074A0640EDD661B1E46E6DB6C13 (UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Networking.UnityWebRequest::get_isNetworkError()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool UnityWebRequest_get_isNetworkError_m036684411466688E71E67CDD3703BAC9035A56F0 (UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Networking.UnityWebRequest::get_isHttpError()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool UnityWebRequest_get_isHttpError_m945BA480A179E05CC9659846414D9521ED648ED5 (UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Networking.UnityWebRequest::get_isDone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool UnityWebRequest_get_isDone_m3079B53A1CAFD8D5B334C635761E7B7E10B14123 (UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* __this, const RuntimeMethod* method) ;
// UnityEngine.Networking.DownloadHandler UnityEngine.Networking.UnityWebRequest::get_downloadHandler()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB* UnityWebRequest_get_downloadHandler_m1AA91B23D9D594A4F4FE2975FC356C508528F1D5 (UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* __this, const RuntimeMethod* method) ;
// System.Byte[] UnityEngine.Networking.DownloadHandler::get_data()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* DownloadHandler_get_data_m1DC9B4514B12939B090028BF28C6BEF21DE9B6F3 (DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB* __this, const RuntimeMethod* method) ;
// System.String UnityEngine.Application::get_persistentDataPath()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Application_get_persistentDataPath_mC58BD3E1A20732E0A536491DBCAE6505B1624399 (const RuntimeMethod* method) ;
// System.Void System.IO.File::WriteAllBytes(System.String,System.Byte[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void File_WriteAllBytes_mC491031DA14AA9B591F62D6AD0181D090E081077 (String_t* ___path0, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___bytes1, const RuntimeMethod* method) ;
// System.Object AmplitudeNS.MiniJSON.Json/Parser::Parse(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Parser_Parse_m0897F575290D51F285E80A70DDB71A6A8347632E (String_t* ___jsonString0, const RuntimeMethod* method) ;
// System.String AmplitudeNS.MiniJSON.Json/Serializer::Serialize(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Serializer_Serialize_mF754FA742B06F31D9855F2522EE45FEE35BBDAD8 (RuntimeObject* ___obj0, const RuntimeMethod* method) ;
// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.Void System.IO.StringReader::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StringReader__ctor_m72556EC1062F49E05CF41B0825AC7FA2DB2A81C0 (StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* __this, String_t* ___s0, const RuntimeMethod* method) ;
// System.Void AmplitudeNS.MiniJSON.Json/Parser::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Parser__ctor_mE517FCCE14F0F261A604154855B2E685AEC990D6 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, String_t* ___jsonString0, const RuntimeMethod* method) ;
// System.Object AmplitudeNS.MiniJSON.Json/Parser::ParseValue()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Parser_ParseValue_m69ADFF7A3F90A560C3BBF3D17B196216BBE01611 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) ;
// System.Void System.IO.TextReader::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextReader_Dispose_mDCB332EFA06970A9CC7EC4596FCC5220B9512616 (TextReader_tB8D43017CB6BE1633E5A86D64E7757366507C1F7* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.String,System.Object>::.ctor()
inline void Dictionary_2__ctor_mC4F3DF292BAD88F4BF193C49CD689FAEBC4570A9 (Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710* __this, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710*, const RuntimeMethod*))Dictionary_2__ctor_m5B32FBC624618211EB461D59CFBB10E987FD1329_gshared)(__this, method);
}
// AmplitudeNS.MiniJSON.Json/Parser/TOKEN AmplitudeNS.MiniJSON.Json/Parser::get_NextToken()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Parser_get_NextToken_mADD25509957CAF9025E0928F68E86271637203A3 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) ;
// System.String AmplitudeNS.MiniJSON.Json/Parser::ParseString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Parser_ParseString_m8C19E88DDBEB2385C2E6C77D195657DA8ACA348A (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.String,System.Object>::set_Item(TKey,TValue)
inline void Dictionary_2_set_Item_m7CCA97075B48AFB2B97E5A072B94BC7679374341 (Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710* __this, String_t* ___key0, RuntimeObject* ___value1, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710*, String_t*, RuntimeObject*, const RuntimeMethod*))Dictionary_2_set_Item_m1A840355E8EDAECEA9D0C6F5E51B248FAA449CBD_gshared)(__this, ___key0, ___value1, method);
}
// System.Void System.Collections.Generic.List`1<System.Object>::.ctor()
inline void List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690 (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
// System.Object AmplitudeNS.MiniJSON.Json/Parser::ParseByToken(AmplitudeNS.MiniJSON.Json/Parser/TOKEN)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Parser_ParseByToken_m38CF2D3879151135AD1363B696D69F6BAE349AD9 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, int32_t ___token0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Object>::Add(T)
inline void List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___item0, const RuntimeMethod* method)
{
	((  void (*) (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*, RuntimeObject*, const RuntimeMethod*))List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline)(__this, ___item0, method);
}
// System.Object AmplitudeNS.MiniJSON.Json/Parser::ParseNumber()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Parser_ParseNumber_mBF13A88AE39CA73AF9D56CAE6E4D32830D6F3236 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) ;
// System.Collections.Generic.Dictionary`2<System.String,System.Object> AmplitudeNS.MiniJSON.Json/Parser::ParseObject()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710* Parser_ParseObject_mE2CDC67778FC5E8204F784BA9BD08A53A80B2574 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) ;
// System.Collections.Generic.List`1<System.Object> AmplitudeNS.MiniJSON.Json/Parser::ParseArray()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* Parser_ParseArray_m8D44F3FD8873E3B704594AB7D3A60BB44C7BBA23 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) ;
// System.Void System.Text.StringBuilder::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StringBuilder__ctor_m1D99713357DE05DAFA296633639DB55F8C30587D (StringBuilder_t* __this, const RuntimeMethod* method) ;
// System.Char AmplitudeNS.MiniJSON.Json/Parser::get_NextChar()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar Parser_get_NextChar_mAADE98129462252CD3EE9FD09976F3CCA541E004 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) ;
// System.Text.StringBuilder System.Text.StringBuilder::Append(System.Char)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringBuilder_t* StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1 (StringBuilder_t* __this, Il2CppChar ___value0, const RuntimeMethod* method) ;
// System.Int32 System.Convert::ToInt32(System.String,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Convert_ToInt32_mD1B3AFBDA26E52D0382434804364FEF8BA241FB4 (String_t* ___value0, int32_t ___fromBase1, const RuntimeMethod* method) ;
// System.String AmplitudeNS.MiniJSON.Json/Parser::get_NextWord()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Parser_get_NextWord_mFBD96068AF0333AD478C0F61F66EFFE77286E6A6 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) ;
// System.Int32 System.String::IndexOf(System.Char)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t String_IndexOf_mE21E78F35EF4A7768E385A72814C88D22B689966 (String_t* __this, Il2CppChar ___value0, const RuntimeMethod* method) ;
// System.Single System.Single::Parse(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Single_Parse_m621F610BB84997A2E3C4686913F482316CD3E6B8 (String_t* ___s0, const RuntimeMethod* method) ;
// System.Char AmplitudeNS.MiniJSON.Json/Parser::get_PeekChar()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar Parser_get_PeekChar_mCB4B296524971AEAAE42C8F25B008196E5F41E8E (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) ;
// System.Char System.Convert::ToChar(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar Convert_ToChar_mF1B1B205DDEFDE52251235514E7DAFCAB37D1F24 (int32_t ___value0, const RuntimeMethod* method) ;
// System.Void AmplitudeNS.MiniJSON.Json/Parser::EatWhitespace()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Parser_EatWhitespace_mE52A290A5EC4A836EBE0401812A550BED86EA908 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) ;
// System.Boolean System.String::op_Equality(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1 (String_t* ___a0, String_t* ___b1, const RuntimeMethod* method) ;
// System.Void AmplitudeNS.MiniJSON.Json/Serializer::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Serializer__ctor_m40CBCABFB74A8C1E82DF0F678EDC65806A765A63 (Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328* __this, const RuntimeMethod* method) ;
// System.Void AmplitudeNS.MiniJSON.Json/Serializer::SerializeValue(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Serializer_SerializeValue_m4FBEC42A2539CE9F469367033AAFEFEAB2FA7551 (Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328* __this, RuntimeObject* ___value0, const RuntimeMethod* method) ;
// System.Text.StringBuilder System.Text.StringBuilder::Append(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringBuilder_t* StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D (StringBuilder_t* __this, String_t* ___value0, const RuntimeMethod* method) ;
// System.Void AmplitudeNS.MiniJSON.Json/Serializer::SerializeString(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Serializer_SerializeString_m90926F2D28A47048AFE41C71647E1B38CB24864A (Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328* __this, String_t* ___str0, const RuntimeMethod* method) ;
// System.String System.String::ToLower()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_ToLower_m6191ABA3DC514ED47C10BDA23FD0DDCEAE7ACFBD (String_t* __this, const RuntimeMethod* method) ;
// System.Void AmplitudeNS.MiniJSON.Json/Serializer::SerializeArray(System.Collections.IList)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Serializer_SerializeArray_m9743D1022222CEBEDAEBEAF42AFDCF3619E63991 (Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328* __this, RuntimeObject* ___anArray0, const RuntimeMethod* method) ;
// System.Void AmplitudeNS.MiniJSON.Json/Serializer::SerializeObject(System.Collections.IDictionary)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Serializer_SerializeObject_mF46ED6A4B7CF7BDBC5971C2E5302C77B37EE024D (Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328* __this, RuntimeObject* ___obj0, const RuntimeMethod* method) ;
// System.Void AmplitudeNS.MiniJSON.Json/Serializer::SerializeOther(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Serializer_SerializeOther_m512C9EBB20ED31EBC8692C238E9292C2FE996BFF (Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328* __this, RuntimeObject* ___value0, const RuntimeMethod* method) ;
// System.Char[] System.String::ToCharArray()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* String_ToCharArray_m0699A92AA3E744229EF29CB9D943C47DF4FE5B46 (String_t* __this, const RuntimeMethod* method) ;
// System.Int32 System.Convert::ToInt32(System.Char)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Convert_ToInt32_mDBBE9318A7CCE1560974CE93F5BFED9931CF0052 (Il2CppChar ___value0, const RuntimeMethod* method) ;
// System.String System.Convert::ToString(System.Int32,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Convert_ToString_mC3349029FE37EB00B5BFCB1F87022458A3834E35 (int32_t ___value0, int32_t ___toBase1, const RuntimeMethod* method) ;
// System.String System.String::PadLeft(System.Int32,System.Char)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_PadLeft_m99DDD242908E78B71E9631EE66331E8A130EB31F (String_t* __this, int32_t ___totalWidth0, Il2CppChar ___paddingChar1, const RuntimeMethod* method) ;
// System.Globalization.CultureInfo System.Globalization.CultureInfo::get_InvariantCulture()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* CultureInfo_get_InvariantCulture_mD1E96DC845E34B10F78CB744B0CB5D7D63CEB1E6 (const RuntimeMethod* method) ;
// System.String System.Single::ToString(System.IFormatProvider)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Single_ToString_m534852BD7949AA972435783D7B96D0FFB09F6D6A (float* __this, RuntimeObject* ___provider0, const RuntimeMethod* method) ;
// System.Double System.Convert::ToDouble(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double Convert_ToDouble_m86FF4F837721833186E883102C056A35F0860EB0 (RuntimeObject* ___value0, const RuntimeMethod* method) ;
// System.String System.Double::ToString(System.IFormatProvider)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Double_ToString_m4318830D9F771852FDCF21C14CF9E8ABC7E77357 (double* __this, RuntimeObject* ___provider0, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void Facebook.Unity.Example.Pay::GetGui()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Pay_GetGui_m38BDD0E07950F97403560C5AAD4D35F4ABE590AA (Pay_t148A6DF661BCB2E555B081F94ABD62F3B0E8770E* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral6ABFA3236CB30A30DF60B11B078D508BF7265D20);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralC1170DBE5107B0A305F0064F8762968128937776);
		s_Il2CppMethodInitialized = true;
	}
	{
		// this.LabelAndTextField("Product: ", ref this.payProduct);
		String_t** L_0 = (&__this->___payProduct_16);
		ConsoleBase_LabelAndTextField_m72E1BBF75934CE747195CB87737A2D7EFC503A8A(__this, _stringLiteral6ABFA3236CB30A30DF60B11B078D508BF7265D20, L_0, NULL);
		// if (this.Button("Call Pay"))
		bool L_1;
		L_1 = ConsoleBase_Button_m8787498ECF036B9B3B5EB6B2FF49AFA154282D10(__this, _stringLiteralC1170DBE5107B0A305F0064F8762968128937776, NULL);
		if (!L_1)
		{
			goto IL_0024;
		}
	}
	{
		// this.CallFBPay();
		Pay_CallFBPay_m6C1243E4AA248F715366AA5E8996D25431AA2A58(__this, NULL);
	}

IL_0024:
	{
		// GUILayout.Space(10);
		GUILayout_Space_m9254FBF173F9260DDB6C83C0066447FC9D9CA597((10.0f), NULL);
		// }
		return;
	}
}
// System.Void Facebook.Unity.Example.Pay::CallFBPay()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Pay_CallFBPay_m6C1243E4AA248F715366AA5E8996D25431AA2A58 (Pay_t148A6DF661BCB2E555B081F94ABD62F3B0E8770E* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&FacebookDelegate_1_t196A2AB9CCB2BC5DCA5BC05F82516E4C3FF9DD4B_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MenuBase_HandleResult_m0CB446ED4B8BDA605B140E61A4A1C6B442765E65_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral4DABC811CE0734D4532568DFC1CDF83347282E52);
		s_Il2CppMethodInitialized = true;
	}
	FacebookDelegate_1_t196A2AB9CCB2BC5DCA5BC05F82516E4C3FF9DD4B* V_0 = NULL;
	Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		// FB.Canvas.Pay(this.payProduct, callback: this.HandleResult);
		String_t* L_0 = __this->___payProduct_16;
		FacebookDelegate_1_t196A2AB9CCB2BC5DCA5BC05F82516E4C3FF9DD4B* L_1 = (FacebookDelegate_1_t196A2AB9CCB2BC5DCA5BC05F82516E4C3FF9DD4B*)il2cpp_codegen_object_new(FacebookDelegate_1_t196A2AB9CCB2BC5DCA5BC05F82516E4C3FF9DD4B_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		FacebookDelegate_1__ctor_mC91675ED1A36D58BAA4B5C50CA96AB135B6CB444(L_1, __this, (intptr_t)((void*)MenuBase_HandleResult_m0CB446ED4B8BDA605B140E61A4A1C6B442765E65_RuntimeMethod_var), NULL);
		V_0 = L_1;
		il2cpp_codegen_initobj((&V_1), sizeof(Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28));
		Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 L_2 = V_1;
		il2cpp_codegen_initobj((&V_1), sizeof(Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28));
		Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 L_3 = V_1;
		FacebookDelegate_1_t196A2AB9CCB2BC5DCA5BC05F82516E4C3FF9DD4B* L_4 = V_0;
		Canvas_Pay_m0C127FE00EDA7742A93998ACAB67A65F3103539A(L_0, _stringLiteral4DABC811CE0734D4532568DFC1CDF83347282E52, 1, L_2, L_3, (String_t*)NULL, (String_t*)NULL, (String_t*)NULL, L_4, NULL);
		// }
		return;
	}
}
// System.Void Facebook.Unity.Example.Pay::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Pay__ctor_mF524FF0E64FFA62C166094B9CFE640D276161A26 (Pay_t148A6DF661BCB2E555B081F94ABD62F3B0E8770E* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&String_t_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// private string payProduct = string.Empty;
		String_t* L_0 = ((String_t_StaticFields*)il2cpp_codegen_static_fields_for(String_t_il2cpp_TypeInfo_var))->___Empty_6;
		__this->___payProduct_16 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___payProduct_16), (void*)L_0);
		MenuBase__ctor_mD1A153F6EBCE35B5990669A20787B391A5DEC0E2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void Facebook.Unity.Example.TournamentsMenu::GetGui()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TournamentsMenu_GetGui_m2DB7B4FBCBB150B9A9AA56743F8AD986EAFDA459 (TournamentsMenu_tED34C4D1C2BA6799532A19C23CE1F9DB480C9D1D* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&FB_tD6AF917A642BEC6920761C8E4AD4013414829013_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&FacebookDelegate_1_t2CCD4851FD0B117C9DDD8076206645B712D37148_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&FacebookDelegate_1_t474682078C474498C8D4805F13E9077763B39255_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&FacebookDelegate_1_t64F37B84FFAB406D45358FC281716C5AD6B6916A_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GUI_tA9CDB3D69DB13D51AD83ABDB587EF95947EC2D2A_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MenuBase_HandleResult_m0CB446ED4B8BDA605B140E61A4A1C6B442765E65_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral225A8B3D65BD41BF9C60DCB20327D49367398223);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral27B1240786558BB01BAF8EA86CF65C4996DEF092);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral41CD180653F0DF900B8BF3930244033A52367BAD);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral524D55BD7CC5730C0A3F5924BDA722299B8D0110);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral68C98D04ED10D22B52DAA3991A073059D36C5412);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral9A5719B6A850747313EBC7FA287B75B9EAD4024B);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralAD9219CAA18C36507564E54553997A48F35A0715);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralEA02C841CE2CA1F91740BD3EA483BF6971E48B9B);
		s_Il2CppMethodInitialized = true;
	}
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D V_0;
	memset((&V_0), 0, sizeof(V_0));
	bool G_B2_0 = false;
	bool G_B1_0 = false;
	int32_t G_B3_0 = 0;
	bool G_B3_1 = false;
	bool G_B5_0 = false;
	bool G_B4_0 = false;
	bool G_B7_0 = false;
	bool G_B6_0 = false;
	bool G_B9_0 = false;
	bool G_B8_0 = false;
	bool G_B11_0 = false;
	bool G_B10_0 = false;
	{
		// bool enabled = GUI.enabled;
		il2cpp_codegen_runtime_class_init_inline(GUI_tA9CDB3D69DB13D51AD83ABDB587EF95947EC2D2A_il2cpp_TypeInfo_var);
		bool L_0;
		L_0 = GUI_get_enabled_m336E115A84DBD8D18A925D0755B51746B98B516D(NULL);
		// GUI.enabled = enabled && FB.IsLoggedIn;
		bool L_1 = L_0;
		G_B1_0 = L_1;
		if (!L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(FB_tD6AF917A642BEC6920761C8E4AD4013414829013_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = FB_get_IsLoggedIn_m0E22CD5EE863598BFBBB29D65F36245A15B9302A(NULL);
		G_B3_0 = ((int32_t)(L_2));
		G_B3_1 = G_B1_0;
		goto IL_0010;
	}

IL_000f:
	{
		G_B3_0 = 0;
		G_B3_1 = G_B2_0;
	}

IL_0010:
	{
		il2cpp_codegen_runtime_class_init_inline(GUI_tA9CDB3D69DB13D51AD83ABDB587EF95947EC2D2A_il2cpp_TypeInfo_var);
		GUI_set_enabled_mF2F99A6870ACAFAEFB5E8FF1B69C684951D390C9((bool)G_B3_0, NULL);
		// if (this.Button("Get Tournaments"))
		bool L_3;
		L_3 = ConsoleBase_Button_m8787498ECF036B9B3B5EB6B2FF49AFA154282D10(__this, _stringLiteralAD9219CAA18C36507564E54553997A48F35A0715, NULL);
		G_B4_0 = G_B3_1;
		if (!L_3)
		{
			G_B5_0 = G_B3_1;
			goto IL_0033;
		}
	}
	{
		// FB.Mobile.GetTournaments(this.HandleResult);
		FacebookDelegate_1_t64F37B84FFAB406D45358FC281716C5AD6B6916A* L_4 = (FacebookDelegate_1_t64F37B84FFAB406D45358FC281716C5AD6B6916A*)il2cpp_codegen_object_new(FacebookDelegate_1_t64F37B84FFAB406D45358FC281716C5AD6B6916A_il2cpp_TypeInfo_var);
		NullCheck(L_4);
		FacebookDelegate_1__ctor_mC41A149F693D6BF00F1D38ED5EC2368B2B5A1328(L_4, __this, (intptr_t)((void*)MenuBase_HandleResult_m0CB446ED4B8BDA605B140E61A4A1C6B442765E65_RuntimeMethod_var), NULL);
		Mobile_GetTournaments_mBA4EED8CDA8C21A64C1A201275C3C34F71F8F12C(L_4, NULL);
		G_B5_0 = G_B4_0;
	}

IL_0033:
	{
		// GUILayout.Space(24);
		GUILayout_Space_m9254FBF173F9260DDB6C83C0066447FC9D9CA597((24.0f), NULL);
		// this.LabelAndTextField("Score:", ref this.score);
		String_t** L_5 = (&__this->___score_16);
		ConsoleBase_LabelAndTextField_m72E1BBF75934CE747195CB87737A2D7EFC503A8A(__this, _stringLiteral27B1240786558BB01BAF8EA86CF65C4996DEF092, L_5, NULL);
		// this.LabelAndTextField("TournamentID:", ref this.tournamentID);
		String_t** L_6 = (&__this->___tournamentID_17);
		ConsoleBase_LabelAndTextField_m72E1BBF75934CE747195CB87737A2D7EFC503A8A(__this, _stringLiteral41CD180653F0DF900B8BF3930244033A52367BAD, L_6, NULL);
		// if (this.Button("Post Score to Tournament"))
		bool L_7;
		L_7 = ConsoleBase_Button_m8787498ECF036B9B3B5EB6B2FF49AFA154282D10(__this, _stringLiteralEA02C841CE2CA1F91740BD3EA483BF6971E48B9B, NULL);
		G_B6_0 = G_B5_0;
		if (!L_7)
		{
			G_B7_0 = G_B5_0;
			goto IL_008e;
		}
	}
	{
		// FB.Mobile.UpdateTournament(tournamentID, int.Parse(score), this.HandleResult);
		String_t* L_8 = __this->___tournamentID_17;
		String_t* L_9 = __this->___score_16;
		int32_t L_10;
		L_10 = Int32_Parse_m273CA1A9C7717C99641291A95C543711C0202AF0(L_9, NULL);
		FacebookDelegate_1_t474682078C474498C8D4805F13E9077763B39255* L_11 = (FacebookDelegate_1_t474682078C474498C8D4805F13E9077763B39255*)il2cpp_codegen_object_new(FacebookDelegate_1_t474682078C474498C8D4805F13E9077763B39255_il2cpp_TypeInfo_var);
		NullCheck(L_11);
		FacebookDelegate_1__ctor_mE1DA08C38DC193E943FBB7C052938E717EABBD4A(L_11, __this, (intptr_t)((void*)MenuBase_HandleResult_m0CB446ED4B8BDA605B140E61A4A1C6B442765E65_RuntimeMethod_var), NULL);
		Mobile_UpdateTournament_mA3EB37B93D1C30F8F320EBFE704AEE759A96194A(L_8, L_10, L_11, NULL);
		G_B7_0 = G_B6_0;
	}

IL_008e:
	{
		// if (this.Button("Update Tournament and Share"))
		bool L_12;
		L_12 = ConsoleBase_Button_m8787498ECF036B9B3B5EB6B2FF49AFA154282D10(__this, _stringLiteral68C98D04ED10D22B52DAA3991A073059D36C5412, NULL);
		G_B8_0 = G_B7_0;
		if (!L_12)
		{
			G_B9_0 = G_B7_0;
			goto IL_00bd;
		}
	}
	{
		// FB.Mobile.UpdateAndShareTournament(tournamentID, int.Parse(score), this.HandleResult);
		String_t* L_13 = __this->___tournamentID_17;
		String_t* L_14 = __this->___score_16;
		int32_t L_15;
		L_15 = Int32_Parse_m273CA1A9C7717C99641291A95C543711C0202AF0(L_14, NULL);
		FacebookDelegate_1_t2CCD4851FD0B117C9DDD8076206645B712D37148* L_16 = (FacebookDelegate_1_t2CCD4851FD0B117C9DDD8076206645B712D37148*)il2cpp_codegen_object_new(FacebookDelegate_1_t2CCD4851FD0B117C9DDD8076206645B712D37148_il2cpp_TypeInfo_var);
		NullCheck(L_16);
		FacebookDelegate_1__ctor_mB15E6C38086F8341B29BE751BD0F95FA93E57793(L_16, __this, (intptr_t)((void*)MenuBase_HandleResult_m0CB446ED4B8BDA605B140E61A4A1C6B442765E65_RuntimeMethod_var), NULL);
		Mobile_UpdateAndShareTournament_m49A8A9E8EA3501EA782E3ADBE9789A180663D5E3(L_13, L_15, L_16, NULL);
		G_B9_0 = G_B8_0;
	}

IL_00bd:
	{
		// if (this.Button("Create Tournament and Share"))
		bool L_17;
		L_17 = ConsoleBase_Button_m8787498ECF036B9B3B5EB6B2FF49AFA154282D10(__this, _stringLiteral9A5719B6A850747313EBC7FA287B75B9EAD4024B, NULL);
		G_B10_0 = G_B9_0;
		if (!L_17)
		{
			G_B11_0 = G_B9_0;
			goto IL_0108;
		}
	}
	{
		// FB.Mobile.CreateAndShareTournament(
		//     int.Parse(score),
		//     "Unity Tournament",
		//     TournamentSortOrder.HigherIsBetter,
		//     TournamentScoreFormat.Numeric,
		//     DateTime.UtcNow.AddHours(2),
		//     "Unity SDK Tournament",
		//     this.HandleResult
		// );
		String_t* L_18 = __this->___score_16;
		int32_t L_19;
		L_19 = Int32_Parse_m273CA1A9C7717C99641291A95C543711C0202AF0(L_18, NULL);
		il2cpp_codegen_runtime_class_init_inline(DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_il2cpp_TypeInfo_var);
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_20;
		L_20 = DateTime_get_UtcNow_m06B6E9995FE16846A0F71EC9DB23E90BE2C5F9FA(NULL);
		V_0 = L_20;
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_21;
		L_21 = DateTime_AddHours_m99C41C078F2F480BF9965F8A4BAB8C8B75C39C02((&V_0), (2.0), NULL);
		FacebookDelegate_1_t2CCD4851FD0B117C9DDD8076206645B712D37148* L_22 = (FacebookDelegate_1_t2CCD4851FD0B117C9DDD8076206645B712D37148*)il2cpp_codegen_object_new(FacebookDelegate_1_t2CCD4851FD0B117C9DDD8076206645B712D37148_il2cpp_TypeInfo_var);
		NullCheck(L_22);
		FacebookDelegate_1__ctor_mB15E6C38086F8341B29BE751BD0F95FA93E57793(L_22, __this, (intptr_t)((void*)MenuBase_HandleResult_m0CB446ED4B8BDA605B140E61A4A1C6B442765E65_RuntimeMethod_var), NULL);
		Mobile_CreateAndShareTournament_m41E95D4C33D17E97E0D70284DBAC5E1B2736930F(L_19, _stringLiteral225A8B3D65BD41BF9C60DCB20327D49367398223, 0, 0, L_21, _stringLiteral524D55BD7CC5730C0A3F5924BDA722299B8D0110, L_22, NULL);
		G_B11_0 = G_B10_0;
	}

IL_0108:
	{
		// GUI.enabled = enabled;
		il2cpp_codegen_runtime_class_init_inline(GUI_tA9CDB3D69DB13D51AD83ABDB587EF95947EC2D2A_il2cpp_TypeInfo_var);
		GUI_set_enabled_mF2F99A6870ACAFAEFB5E8FF1B69C684951D390C9(G_B11_0, NULL);
		// }
		return;
	}
}
// System.Void Facebook.Unity.Example.TournamentsMenu::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TournamentsMenu__ctor_mA3069507B062E55BF40EA9C97FEDBF5C64B69705 (TournamentsMenu_tED34C4D1C2BA6799532A19C23CE1F9DB480C9D1D* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&String_t_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// private string score = string.Empty;
		String_t* L_0 = ((String_t_StaticFields*)il2cpp_codegen_static_fields_for(String_t_il2cpp_TypeInfo_var))->___Empty_6;
		__this->___score_16 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___score_16), (void*)L_0);
		// private string tournamentID = string.Empty;
		String_t* L_1 = ((String_t_StaticFields*)il2cpp_codegen_static_fields_for(String_t_il2cpp_TypeInfo_var))->___Empty_6;
		__this->___tournamentID_17 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___tournamentID_17), (void*)L_1);
		MenuBase__ctor_mD1A153F6EBCE35B5990669A20787B391A5DEC0E2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void Facebook.Unity.Example.UploadToMediaLibrary::GetGui()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UploadToMediaLibrary_GetGui_m420ECCD445E271C81786F4637EBFBB9F9C44B5D9 (UploadToMediaLibrary_t7C93E0DCCC3E19053E2626472FF45E6A9036FFCE* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Array_Empty_TisGUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14_mC7F345AC4C0CA86560FAA00174268F70FBBE577F_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&FB_tD6AF917A642BEC6920761C8E4AD4013414829013_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&FacebookDelegate_1_t7CA00F6A27B3FE85139590EFEA03B7C7C0D4A66D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GUI_tA9CDB3D69DB13D51AD83ABDB587EF95947EC2D2A_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MenuBase_HandleResult_m0CB446ED4B8BDA605B140E61A4A1C6B442765E65_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral0FC1909DFE6F4E0D07D99837B774F71D87B79DDD);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral1774E11D04A024FCDA7A4FDD51BD9A11A06BB98F);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5022C548F0BA2E87BCC042A74207361A4FCEDF6F);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5E78319061735653C64261371943DC0AE10A90EC);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral9CA60916954D1B73462DE743639589D43D096E57);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral9D87FDDD090A10F9879AC40F0B67308856B5FB68);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB13730B3D9E1D51656CEC99169825C5929F0E430);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDA13B1590448DE660CB50261E351C16C14D5CBBB);
		s_Il2CppMethodInitialized = true;
	}
	String_t* V_0 = NULL;
	String_t* V_1 = NULL;
	int32_t G_B3_0 = 0;
	{
		// bool enabled = GUI.enabled;
		il2cpp_codegen_runtime_class_init_inline(GUI_tA9CDB3D69DB13D51AD83ABDB587EF95947EC2D2A_il2cpp_TypeInfo_var);
		bool L_0;
		L_0 = GUI_get_enabled_m336E115A84DBD8D18A925D0755B51746B98B516D(NULL);
		// GUI.enabled = enabled && FB.IsLoggedIn;
		if (!L_0)
		{
			goto IL_000e;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(FB_tD6AF917A642BEC6920761C8E4AD4013414829013_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = FB_get_IsLoggedIn_m0E22CD5EE863598BFBBB29D65F36245A15B9302A(NULL);
		G_B3_0 = ((int32_t)(L_1));
		goto IL_000f;
	}

IL_000e:
	{
		G_B3_0 = 0;
	}

IL_000f:
	{
		il2cpp_codegen_runtime_class_init_inline(GUI_tA9CDB3D69DB13D51AD83ABDB587EF95947EC2D2A_il2cpp_TypeInfo_var);
		GUI_set_enabled_mF2F99A6870ACAFAEFB5E8FF1B69C684951D390C9((bool)G_B3_0, NULL);
		// GUILayout.Space(24);
		GUILayout_Space_m9254FBF173F9260DDB6C83C0066447FC9D9CA597((24.0f), NULL);
		// this.LabelAndTextField("Image caption:", ref this.imageCaption);
		String_t** L_2 = (&__this->___imageCaption_17);
		ConsoleBase_LabelAndTextField_m72E1BBF75934CE747195CB87737A2D7EFC503A8A(__this, _stringLiteral9CA60916954D1B73462DE743639589D43D096E57, L_2, NULL);
		// if (this.Button("Image Dialog: " + imageShouldLaunchMediaDialog.ToString()))
		bool* L_3 = (&__this->___imageShouldLaunchMediaDialog_16);
		String_t* L_4;
		L_4 = Boolean_ToString_m6646C8026B1DF381A1EE8CD13549175E9703CC63(L_3, NULL);
		String_t* L_5;
		L_5 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteral9D87FDDD090A10F9879AC40F0B67308856B5FB68, L_4, NULL);
		bool L_6;
		L_6 = ConsoleBase_Button_m8787498ECF036B9B3B5EB6B2FF49AFA154282D10(__this, L_5, NULL);
		if (!L_6)
		{
			goto IL_005b;
		}
	}
	{
		// imageShouldLaunchMediaDialog = !imageShouldLaunchMediaDialog;
		bool L_7 = __this->___imageShouldLaunchMediaDialog_16;
		__this->___imageShouldLaunchMediaDialog_16 = (bool)((((int32_t)L_7) == ((int32_t)0))? 1 : 0);
	}

IL_005b:
	{
		// GUILayout.Space(24);
		GUILayout_Space_m9254FBF173F9260DDB6C83C0066447FC9D9CA597((24.0f), NULL);
		// string imagePath = GetPath(imageFile);
		String_t* L_8 = __this->___imageFile_18;
		String_t* L_9;
		L_9 = UploadToMediaLibrary_GetPath_mDBF5060B2EC32877F865F24B6D97C420B7610760(__this, L_8, NULL);
		V_0 = L_9;
		// if (File.Exists(imagePath))
		String_t* L_10 = V_0;
		bool L_11;
		L_11 = File_Exists_m95E329ABBE3EAD6750FE1989BBA6884457136D4A(L_10, NULL);
		if (!L_11)
		{
			goto IL_00ac;
		}
	}
	{
		// if (this.Button("Upload Image to media library"))
		bool L_12;
		L_12 = ConsoleBase_Button_m8787498ECF036B9B3B5EB6B2FF49AFA154282D10(__this, _stringLiteralB13730B3D9E1D51656CEC99169825C5929F0E430, NULL);
		if (!L_12)
		{
			goto IL_00c1;
		}
	}
	{
		// FBGamingServices.UploadImageToMediaLibrary(imageCaption, new Uri(imagePath), imageShouldLaunchMediaDialog, this.HandleResult);
		String_t* L_13 = __this->___imageCaption_17;
		String_t* L_14 = V_0;
		Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E* L_15 = (Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E*)il2cpp_codegen_object_new(Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E_il2cpp_TypeInfo_var);
		NullCheck(L_15);
		Uri__ctor_m6CA436E6AD2768A121FA851CBEEFA3623E849D3A(L_15, L_14, NULL);
		bool L_16 = __this->___imageShouldLaunchMediaDialog_16;
		FacebookDelegate_1_t7CA00F6A27B3FE85139590EFEA03B7C7C0D4A66D* L_17 = (FacebookDelegate_1_t7CA00F6A27B3FE85139590EFEA03B7C7C0D4A66D*)il2cpp_codegen_object_new(FacebookDelegate_1_t7CA00F6A27B3FE85139590EFEA03B7C7C0D4A66D_il2cpp_TypeInfo_var);
		NullCheck(L_17);
		FacebookDelegate_1__ctor_m8A5848CA226A79CA3779B0A21AC0E1DDEF7908F7(L_17, __this, (intptr_t)((void*)MenuBase_HandleResult_m0CB446ED4B8BDA605B140E61A4A1C6B442765E65_RuntimeMethod_var), NULL);
		FBGamingServices_UploadImageToMediaLibrary_m834FB0476EA0E1777F7509239AF063283A9A87BF(L_13, L_15, L_16, L_17, NULL);
		goto IL_00c1;
	}

IL_00ac:
	{
		// GUILayout.Label("Image does not exist: " + imagePath);
		String_t* L_18 = V_0;
		String_t* L_19;
		L_19 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteral1774E11D04A024FCDA7A4FDD51BD9A11A06BB98F, L_18, NULL);
		GUILayoutOptionU5BU5D_t24AB80AB9355D784F2C65E12A4D0CC2E0C914CA2* L_20;
		L_20 = Array_Empty_TisGUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14_mC7F345AC4C0CA86560FAA00174268F70FBBE577F_inline(Array_Empty_TisGUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14_mC7F345AC4C0CA86560FAA00174268F70FBBE577F_RuntimeMethod_var);
		GUILayout_Label_m1709C16A433383CCFC1FEA0E585E14CBD78CD94B(L_19, L_20, NULL);
	}

IL_00c1:
	{
		// GUILayout.Space(24);
		GUILayout_Space_m9254FBF173F9260DDB6C83C0066447FC9D9CA597((24.0f), NULL);
		// this.LabelAndTextField("Video caption:", ref this.videoCaption);
		String_t** L_21 = (&__this->___videoCaption_20);
		ConsoleBase_LabelAndTextField_m72E1BBF75934CE747195CB87737A2D7EFC503A8A(__this, _stringLiteral5E78319061735653C64261371943DC0AE10A90EC, L_21, NULL);
		// if (this.Button("Video Dialog: " + videoShouldLaunchMediaDialog.ToString()))
		bool* L_22 = (&__this->___videoShouldLaunchMediaDialog_19);
		String_t* L_23;
		L_23 = Boolean_ToString_m6646C8026B1DF381A1EE8CD13549175E9703CC63(L_22, NULL);
		String_t* L_24;
		L_24 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteral0FC1909DFE6F4E0D07D99837B774F71D87B79DDD, L_23, NULL);
		bool L_25;
		L_25 = ConsoleBase_Button_m8787498ECF036B9B3B5EB6B2FF49AFA154282D10(__this, L_24, NULL);
		if (!L_25)
		{
			goto IL_0108;
		}
	}
	{
		// videoShouldLaunchMediaDialog = !videoShouldLaunchMediaDialog;
		bool L_26 = __this->___videoShouldLaunchMediaDialog_19;
		__this->___videoShouldLaunchMediaDialog_19 = (bool)((((int32_t)L_26) == ((int32_t)0))? 1 : 0);
	}

IL_0108:
	{
		// GUILayout.Space(24);
		GUILayout_Space_m9254FBF173F9260DDB6C83C0066447FC9D9CA597((24.0f), NULL);
		// string videoPath = GetPath(videoFile);
		String_t* L_27 = __this->___videoFile_21;
		String_t* L_28;
		L_28 = UploadToMediaLibrary_GetPath_mDBF5060B2EC32877F865F24B6D97C420B7610760(__this, L_27, NULL);
		V_1 = L_28;
		// if (File.Exists(videoPath))
		String_t* L_29 = V_1;
		bool L_30;
		L_30 = File_Exists_m95E329ABBE3EAD6750FE1989BBA6884457136D4A(L_29, NULL);
		if (!L_30)
		{
			goto IL_0158;
		}
	}
	{
		// if (this.Button("Upload Video to media library"))
		bool L_31;
		L_31 = ConsoleBase_Button_m8787498ECF036B9B3B5EB6B2FF49AFA154282D10(__this, _stringLiteralDA13B1590448DE660CB50261E351C16C14D5CBBB, NULL);
		if (!L_31)
		{
			goto IL_016d;
		}
	}
	{
		// FBGamingServices.UploadVideoToMediaLibrary(videoCaption, new Uri(videoPath), videoShouldLaunchMediaDialog, this.HandleResult);
		String_t* L_32 = __this->___videoCaption_20;
		String_t* L_33 = V_1;
		Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E* L_34 = (Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E*)il2cpp_codegen_object_new(Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E_il2cpp_TypeInfo_var);
		NullCheck(L_34);
		Uri__ctor_m6CA436E6AD2768A121FA851CBEEFA3623E849D3A(L_34, L_33, NULL);
		bool L_35 = __this->___videoShouldLaunchMediaDialog_19;
		FacebookDelegate_1_t7CA00F6A27B3FE85139590EFEA03B7C7C0D4A66D* L_36 = (FacebookDelegate_1_t7CA00F6A27B3FE85139590EFEA03B7C7C0D4A66D*)il2cpp_codegen_object_new(FacebookDelegate_1_t7CA00F6A27B3FE85139590EFEA03B7C7C0D4A66D_il2cpp_TypeInfo_var);
		NullCheck(L_36);
		FacebookDelegate_1__ctor_m8A5848CA226A79CA3779B0A21AC0E1DDEF7908F7(L_36, __this, (intptr_t)((void*)MenuBase_HandleResult_m0CB446ED4B8BDA605B140E61A4A1C6B442765E65_RuntimeMethod_var), NULL);
		FBGamingServices_UploadVideoToMediaLibrary_m1ADD423D1D4A24824D688D23211014B7F121F066(L_32, L_34, L_35, L_36, NULL);
		return;
	}

IL_0158:
	{
		// GUILayout.Label("Video does not exist: " + videoPath);
		String_t* L_37 = V_1;
		String_t* L_38;
		L_38 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteral5022C548F0BA2E87BCC042A74207361A4FCEDF6F, L_37, NULL);
		GUILayoutOptionU5BU5D_t24AB80AB9355D784F2C65E12A4D0CC2E0C914CA2* L_39;
		L_39 = Array_Empty_TisGUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14_mC7F345AC4C0CA86560FAA00174268F70FBBE577F_inline(Array_Empty_TisGUILayoutOption_t8B0AA056521747053A3176FCC43E9C3608940A14_mC7F345AC4C0CA86560FAA00174268F70FBBE577F_RuntimeMethod_var);
		GUILayout_Label_m1709C16A433383CCFC1FEA0E585E14CBD78CD94B(L_38, L_39, NULL);
	}

IL_016d:
	{
		// }
		return;
	}
}
// System.String Facebook.Unity.Example.UploadToMediaLibrary::GetPath(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* UploadToMediaLibrary_GetPath_mDBF5060B2EC32877F865F24B6D97C420B7610760 (UploadToMediaLibrary_t7C93E0DCCC3E19053E2626472FF45E6A9036FFCE* __this, String_t* ___filename0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* V_0 = NULL;
	UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* V_1 = NULL;
	{
		// string path = Path.Combine(Application.streamingAssetsPath, filename);
		String_t* L_0;
		L_0 = Application_get_streamingAssetsPath_mB904BCD9A7A4F18A52C175DE4A81F5DC3010CDB5(NULL);
		String_t* L_1 = ___filename0;
		il2cpp_codegen_runtime_class_init_inline(Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var);
		String_t* L_2;
		L_2 = Path_Combine_m1ADAC05CDA2D1D61B172DF65A81E86592696BEAE(L_0, L_1, NULL);
		// byte[] data = null;
		V_0 = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)NULL;
		// var request = UnityWebRequest.Get(path);
		UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* L_3;
		L_3 = UnityWebRequest_Get_m1A332EE069BB5052368307F254A5A7627BB5FD86(L_2, NULL);
		V_1 = L_3;
		// request.SendWebRequest();
		UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* L_4 = V_1;
		NullCheck(L_4);
		UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C* L_5;
		L_5 = UnityWebRequest_SendWebRequest_mA3CD13983BAA5074A0640EDD661B1E46E6DB6C13(L_4, NULL);
		goto IL_002c;
	}

IL_001c:
	{
		// if (request.isNetworkError || request.isHttpError) {
		UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* L_6 = V_1;
		NullCheck(L_6);
		bool L_7;
		L_7 = UnityWebRequest_get_isNetworkError_m036684411466688E71E67CDD3703BAC9035A56F0(L_6, NULL);
		if (L_7)
		{
			goto IL_0034;
		}
	}
	{
		UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* L_8 = V_1;
		NullCheck(L_8);
		bool L_9;
		L_9 = UnityWebRequest_get_isHttpError_m945BA480A179E05CC9659846414D9521ED648ED5(L_8, NULL);
		if (L_9)
		{
			goto IL_0034;
		}
	}

IL_002c:
	{
		// while (!request.isDone) {
		UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* L_10 = V_1;
		NullCheck(L_10);
		bool L_11;
		L_11 = UnityWebRequest_get_isDone_m3079B53A1CAFD8D5B334C635761E7B7E10B14123(L_10, NULL);
		if (!L_11)
		{
			goto IL_001c;
		}
	}

IL_0034:
	{
		// data = request.downloadHandler.data;
		UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* L_12 = V_1;
		NullCheck(L_12);
		DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB* L_13;
		L_13 = UnityWebRequest_get_downloadHandler_m1AA91B23D9D594A4F4FE2975FC356C508528F1D5(L_12, NULL);
		NullCheck(L_13);
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_14;
		L_14 = DownloadHandler_get_data_m1DC9B4514B12939B090028BF28C6BEF21DE9B6F3(L_13, NULL);
		V_0 = L_14;
		// path = Path.Combine(Application.persistentDataPath, filename);
		String_t* L_15;
		L_15 = Application_get_persistentDataPath_mC58BD3E1A20732E0A536491DBCAE6505B1624399(NULL);
		String_t* L_16 = ___filename0;
		il2cpp_codegen_runtime_class_init_inline(Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var);
		String_t* L_17;
		L_17 = Path_Combine_m1ADAC05CDA2D1D61B172DF65A81E86592696BEAE(L_15, L_16, NULL);
		// System.IO.File.WriteAllBytes(path, data);
		String_t* L_18 = L_17;
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_19 = V_0;
		File_WriteAllBytes_mC491031DA14AA9B591F62D6AD0181D090E081077(L_18, L_19, NULL);
		// return path;
		return L_18;
	}
}
// System.Void Facebook.Unity.Example.UploadToMediaLibrary::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UploadToMediaLibrary__ctor_m4AA178830775ECF0D746C2C99DE10BA396FE4FB3 (UploadToMediaLibrary_t7C93E0DCCC3E19053E2626472FF45E6A9036FFCE* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral10937E4A05E6010BC7766B4D68EC8E55E43FC0E4);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral8A71530774D0DEAE024B0BA9D9C9AF1E058E3D29);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralA1CB83ED6E8FF560D57C5DE5A8E1CD5AAE2DC401);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralCB1A1E559DB1A4EA52211DCF0A2326F92FFF89D2);
		s_Il2CppMethodInitialized = true;
	}
	{
		// private bool imageShouldLaunchMediaDialog = true;
		__this->___imageShouldLaunchMediaDialog_16 = (bool)1;
		// private string imageCaption = "Image Caption";
		__this->___imageCaption_17 = _stringLiteral10937E4A05E6010BC7766B4D68EC8E55E43FC0E4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___imageCaption_17), (void*)_stringLiteral10937E4A05E6010BC7766B4D68EC8E55E43FC0E4);
		// private string imageFile = "meta-logo.png";
		__this->___imageFile_18 = _stringLiteral8A71530774D0DEAE024B0BA9D9C9AF1E058E3D29;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___imageFile_18), (void*)_stringLiteral8A71530774D0DEAE024B0BA9D9C9AF1E058E3D29);
		// private bool videoShouldLaunchMediaDialog = true;
		__this->___videoShouldLaunchMediaDialog_19 = (bool)1;
		// private string videoCaption = "Video Caption";
		__this->___videoCaption_20 = _stringLiteralA1CB83ED6E8FF560D57C5DE5A8E1CD5AAE2DC401;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___videoCaption_20), (void*)_stringLiteralA1CB83ED6E8FF560D57C5DE5A8E1CD5AAE2DC401);
		// private string videoFile = "meta.mp4";
		__this->___videoFile_21 = _stringLiteralCB1A1E559DB1A4EA52211DCF0A2326F92FFF89D2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___videoFile_21), (void*)_stringLiteralCB1A1E559DB1A4EA52211DCF0A2326F92FFF89D2);
		MenuBase__ctor_mD1A153F6EBCE35B5990669A20787B391A5DEC0E2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Object AmplitudeNS.MiniJSON.Json::Deserialize(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Json_Deserialize_m5E420F2F3E6C9B75A8B1A4E4BA47D33CCC332235 (String_t* ___json0, const RuntimeMethod* method) 
{
	{
		// if (json == null) {
		String_t* L_0 = ___json0;
		if (L_0)
		{
			goto IL_0005;
		}
	}
	{
		// return null;
		return NULL;
	}

IL_0005:
	{
		// return Parser.Parse(json);
		String_t* L_1 = ___json0;
		RuntimeObject* L_2;
		L_2 = Parser_Parse_m0897F575290D51F285E80A70DDB71A6A8347632E(L_1, NULL);
		return L_2;
	}
}
// System.String AmplitudeNS.MiniJSON.Json::Serialize(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Json_Serialize_m2A6E4B262C12F39C5A1A1DF83D4F5BB89C62A45D (RuntimeObject* ___obj0, const RuntimeMethod* method) 
{
	{
		// return Serializer.Serialize(obj);
		RuntimeObject* L_0 = ___obj0;
		String_t* L_1;
		L_1 = Serializer_Serialize_mF754FA742B06F31D9855F2522EE45FEE35BBDAD8(L_0, NULL);
		return L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void AmplitudeNS.MiniJSON.Json/Parser::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Parser__ctor_mE517FCCE14F0F261A604154855B2E685AEC990D6 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, String_t* ___jsonString0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Parser(string jsonString) {
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		// json = new StringReader(jsonString);
		String_t* L_0 = ___jsonString0;
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_1 = (StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8*)il2cpp_codegen_object_new(StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		StringReader__ctor_m72556EC1062F49E05CF41B0825AC7FA2DB2A81C0(L_1, L_0, NULL);
		__this->___json_2 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___json_2), (void*)L_1);
		// }
		return;
	}
}
// System.Object AmplitudeNS.MiniJSON.Json/Parser::Parse(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Parser_Parse_m0897F575290D51F285E80A70DDB71A6A8347632E (String_t* ___jsonString0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Parser_t5CE444F52863545C1D883D8E083F9F5C67124951_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* V_0 = NULL;
	RuntimeObject* V_1 = NULL;
	{
		// using (var instance = new Parser(jsonString)) {
		String_t* L_0 = ___jsonString0;
		Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* L_1 = (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951*)il2cpp_codegen_object_new(Parser_t5CE444F52863545C1D883D8E083F9F5C67124951_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		Parser__ctor_mE517FCCE14F0F261A604154855B2E685AEC990D6(L_1, L_0, NULL);
		V_0 = L_1;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0010:
			{// begin finally (depth: 1)
				{
					Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* L_2 = V_0;
					if (!L_2)
					{
						goto IL_0019;
					}
				}
				{
					Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* L_3 = V_0;
					NullCheck(L_3);
					InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_3);
				}

IL_0019:
				{
					return;
				}
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			// return instance.ParseValue();
			Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* L_4 = V_0;
			NullCheck(L_4);
			RuntimeObject* L_5;
			L_5 = Parser_ParseValue_m69ADFF7A3F90A560C3BBF3D17B196216BBE01611(L_4, NULL);
			V_1 = L_5;
			goto IL_001a;
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_001a:
	{
		// }
		RuntimeObject* L_6 = V_1;
		return L_6;
	}
}
// System.Void AmplitudeNS.MiniJSON.Json/Parser::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Parser_Dispose_m113E3EB58DAF2D367D33F0D201D16465A1B98323 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) 
{
	{
		// json.Dispose();
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_0 = __this->___json_2;
		NullCheck(L_0);
		TextReader_Dispose_mDCB332EFA06970A9CC7EC4596FCC5220B9512616(L_0, NULL);
		// json = null;
		__this->___json_2 = (StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___json_2), (void*)(StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8*)NULL);
		// }
		return;
	}
}
// System.Collections.Generic.Dictionary`2<System.String,System.Object> AmplitudeNS.MiniJSON.Json/Parser::ParseObject()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710* Parser_ParseObject_mE2CDC67778FC5E8204F784BA9BD08A53A80B2574 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2__ctor_mC4F3DF292BAD88F4BF193C49CD689FAEBC4570A9_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_set_Item_m7CCA97075B48AFB2B97E5A072B94BC7679374341_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710* V_0 = NULL;
	String_t* V_1 = NULL;
	int32_t V_2 = 0;
	{
		// Dictionary<string, object> table = new Dictionary<string, object>();
		Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710* L_0 = (Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710*)il2cpp_codegen_object_new(Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		Dictionary_2__ctor_mC4F3DF292BAD88F4BF193C49CD689FAEBC4570A9(L_0, Dictionary_2__ctor_mC4F3DF292BAD88F4BF193C49CD689FAEBC4570A9_RuntimeMethod_var);
		V_0 = L_0;
		// json.Read();
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_1 = __this->___json_2;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = VirtualFuncInvoker0< int32_t >::Invoke(10 /* System.Int32 System.IO.TextReader::Read() */, L_1);
	}

IL_0012:
	{
		// switch (NextToken) {
		int32_t L_3;
		L_3 = Parser_get_NextToken_mADD25509957CAF9025E0928F68E86271637203A3(__this, NULL);
		V_2 = L_3;
		int32_t L_4 = V_2;
		if (!L_4)
		{
			goto IL_0026;
		}
	}
	{
		int32_t L_5 = V_2;
		if ((((int32_t)L_5) == ((int32_t)2)))
		{
			goto IL_0028;
		}
	}
	{
		int32_t L_6 = V_2;
		if ((((int32_t)L_6) == ((int32_t)6)))
		{
			goto IL_0012;
		}
	}
	{
		goto IL_002a;
	}

IL_0026:
	{
		// return null;
		return (Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710*)NULL;
	}

IL_0028:
	{
		// return table;
		Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710* L_7 = V_0;
		return L_7;
	}

IL_002a:
	{
		// string name = ParseString();
		String_t* L_8;
		L_8 = Parser_ParseString_m8C19E88DDBEB2385C2E6C77D195657DA8ACA348A(__this, NULL);
		V_1 = L_8;
		// if (name == null) {
		String_t* L_9 = V_1;
		if (L_9)
		{
			goto IL_0036;
		}
	}
	{
		// return null;
		return (Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710*)NULL;
	}

IL_0036:
	{
		// if (NextToken != TOKEN.COLON) {
		int32_t L_10;
		L_10 = Parser_get_NextToken_mADD25509957CAF9025E0928F68E86271637203A3(__this, NULL);
		if ((((int32_t)L_10) == ((int32_t)5)))
		{
			goto IL_0041;
		}
	}
	{
		// return null;
		return (Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710*)NULL;
	}

IL_0041:
	{
		// json.Read();
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_11 = __this->___json_2;
		NullCheck(L_11);
		int32_t L_12;
		L_12 = VirtualFuncInvoker0< int32_t >::Invoke(10 /* System.Int32 System.IO.TextReader::Read() */, L_11);
		// table[name] = ParseValue();
		Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710* L_13 = V_0;
		String_t* L_14 = V_1;
		RuntimeObject* L_15;
		L_15 = Parser_ParseValue_m69ADFF7A3F90A560C3BBF3D17B196216BBE01611(__this, NULL);
		NullCheck(L_13);
		Dictionary_2_set_Item_m7CCA97075B48AFB2B97E5A072B94BC7679374341(L_13, L_14, L_15, Dictionary_2_set_Item_m7CCA97075B48AFB2B97E5A072B94BC7679374341_RuntimeMethod_var);
		// break;
		goto IL_0012;
	}
}
// System.Collections.Generic.List`1<System.Object> AmplitudeNS.MiniJSON.Json/Parser::ParseArray()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* Parser_ParseArray_m8D44F3FD8873E3B704594AB7D3A60BB44C7BBA23 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* V_0 = NULL;
	bool V_1 = false;
	int32_t V_2 = 0;
	RuntimeObject* V_3 = NULL;
	{
		// List<object> array = new List<object>();
		List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* L_0 = (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*)il2cpp_codegen_object_new(List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690(L_0, List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_RuntimeMethod_var);
		V_0 = L_0;
		// json.Read();
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_1 = __this->___json_2;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = VirtualFuncInvoker0< int32_t >::Invoke(10 /* System.Int32 System.IO.TextReader::Read() */, L_1);
		// var parsing = true;
		V_1 = (bool)1;
		goto IL_003f;
	}

IL_0016:
	{
		// TOKEN nextToken = NextToken;
		int32_t L_3;
		L_3 = Parser_get_NextToken_mADD25509957CAF9025E0928F68E86271637203A3(__this, NULL);
		V_2 = L_3;
		int32_t L_4 = V_2;
		if (!L_4)
		{
			goto IL_002a;
		}
	}
	{
		int32_t L_5 = V_2;
		if ((((int32_t)L_5) == ((int32_t)4)))
		{
			goto IL_002c;
		}
	}
	{
		int32_t L_6 = V_2;
		if ((((int32_t)L_6) == ((int32_t)6)))
		{
			goto IL_003f;
		}
	}
	{
		goto IL_0030;
	}

IL_002a:
	{
		// return null;
		return (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*)NULL;
	}

IL_002c:
	{
		// parsing = false;
		V_1 = (bool)0;
		// break;
		goto IL_003f;
	}

IL_0030:
	{
		// object value = ParseByToken(nextToken);
		int32_t L_7 = V_2;
		RuntimeObject* L_8;
		L_8 = Parser_ParseByToken_m38CF2D3879151135AD1363B696D69F6BAE349AD9(__this, L_7, NULL);
		V_3 = L_8;
		// array.Add(value);
		List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* L_9 = V_0;
		RuntimeObject* L_10 = V_3;
		NullCheck(L_9);
		List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_inline(L_9, L_10, List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_RuntimeMethod_var);
	}

IL_003f:
	{
		// while (parsing) {
		bool L_11 = V_1;
		if (L_11)
		{
			goto IL_0016;
		}
	}
	{
		// return array;
		List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* L_12 = V_0;
		return L_12;
	}
}
// System.Object AmplitudeNS.MiniJSON.Json/Parser::ParseValue()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Parser_ParseValue_m69ADFF7A3F90A560C3BBF3D17B196216BBE01611 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		// TOKEN nextToken = NextToken;
		int32_t L_0;
		L_0 = Parser_get_NextToken_mADD25509957CAF9025E0928F68E86271637203A3(__this, NULL);
		V_0 = L_0;
		// return ParseByToken(nextToken);
		int32_t L_1 = V_0;
		RuntimeObject* L_2;
		L_2 = Parser_ParseByToken_m38CF2D3879151135AD1363B696D69F6BAE349AD9(__this, L_1, NULL);
		return L_2;
	}
}
// System.Object AmplitudeNS.MiniJSON.Json/Parser::ParseByToken(AmplitudeNS.MiniJSON.Json/Parser/TOKEN)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Parser_ParseByToken_m38CF2D3879151135AD1363B696D69F6BAE349AD9 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, int32_t ___token0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		int32_t L_0 = ___token0;
		switch (((int32_t)il2cpp_codegen_subtract((int32_t)L_0, 1)))
		{
			case 0:
			{
				goto IL_0044;
			}
			case 1:
			{
				goto IL_0062;
			}
			case 2:
			{
				goto IL_004b;
			}
			case 3:
			{
				goto IL_0062;
			}
			case 4:
			{
				goto IL_0062;
			}
			case 5:
			{
				goto IL_0062;
			}
			case 6:
			{
				goto IL_0036;
			}
			case 7:
			{
				goto IL_003d;
			}
			case 8:
			{
				goto IL_0052;
			}
			case 9:
			{
				goto IL_0059;
			}
			case 10:
			{
				goto IL_0060;
			}
		}
	}
	{
		goto IL_0062;
	}

IL_0036:
	{
		// return ParseString();
		String_t* L_1;
		L_1 = Parser_ParseString_m8C19E88DDBEB2385C2E6C77D195657DA8ACA348A(__this, NULL);
		return L_1;
	}

IL_003d:
	{
		// return ParseNumber();
		RuntimeObject* L_2;
		L_2 = Parser_ParseNumber_mBF13A88AE39CA73AF9D56CAE6E4D32830D6F3236(__this, NULL);
		return L_2;
	}

IL_0044:
	{
		// return ParseObject();
		Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710* L_3;
		L_3 = Parser_ParseObject_mE2CDC67778FC5E8204F784BA9BD08A53A80B2574(__this, NULL);
		return L_3;
	}

IL_004b:
	{
		// return ParseArray();
		List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* L_4;
		L_4 = Parser_ParseArray_m8D44F3FD8873E3B704594AB7D3A60BB44C7BBA23(__this, NULL);
		return L_4;
	}

IL_0052:
	{
		// return true;
		bool L_5 = ((bool)1);
		RuntimeObject* L_6 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_5);
		return L_6;
	}

IL_0059:
	{
		// return false;
		bool L_7 = ((bool)0);
		RuntimeObject* L_8 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_7);
		return L_8;
	}

IL_0060:
	{
		// return null;
		return NULL;
	}

IL_0062:
	{
		// return null;
		return NULL;
	}
}
// System.String AmplitudeNS.MiniJSON.Json/Parser::ParseString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Parser_ParseString_m8C19E88DDBEB2385C2E6C77D195657DA8ACA348A (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringBuilder_t_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	StringBuilder_t* V_0 = NULL;
	Il2CppChar V_1 = 0x0;
	bool V_2 = false;
	StringBuilder_t* V_3 = NULL;
	int32_t V_4 = 0;
	{
		// StringBuilder s = new StringBuilder();
		StringBuilder_t* L_0 = (StringBuilder_t*)il2cpp_codegen_object_new(StringBuilder_t_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		StringBuilder__ctor_m1D99713357DE05DAFA296633639DB55F8C30587D(L_0, NULL);
		V_0 = L_0;
		// json.Read();
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_1 = __this->___json_2;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = VirtualFuncInvoker0< int32_t >::Invoke(10 /* System.Int32 System.IO.TextReader::Read() */, L_1);
		// bool parsing = true;
		V_2 = (bool)1;
		goto IL_0139;
	}

IL_0019:
	{
		// if (json.Peek() == -1) {
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_3 = __this->___json_2;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = VirtualFuncInvoker0< int32_t >::Invoke(9 /* System.Int32 System.IO.TextReader::Peek() */, L_3);
		if ((!(((uint32_t)L_4) == ((uint32_t)(-1)))))
		{
			goto IL_002e;
		}
	}
	{
		// parsing = false;
		V_2 = (bool)0;
		// break;
		goto IL_013f;
	}

IL_002e:
	{
		// c = NextChar;
		Il2CppChar L_5;
		L_5 = Parser_get_NextChar_mAADE98129462252CD3EE9FD09976F3CCA541E004(__this, NULL);
		V_1 = L_5;
		Il2CppChar L_6 = V_1;
		if ((((int32_t)L_6) == ((int32_t)((int32_t)34))))
		{
			goto IL_0044;
		}
	}
	{
		Il2CppChar L_7 = V_1;
		if ((((int32_t)L_7) == ((int32_t)((int32_t)92))))
		{
			goto IL_004b;
		}
	}
	{
		goto IL_0131;
	}

IL_0044:
	{
		// parsing = false;
		V_2 = (bool)0;
		// break;
		goto IL_0139;
	}

IL_004b:
	{
		// if (json.Peek() == -1) {
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_8 = __this->___json_2;
		NullCheck(L_8);
		int32_t L_9;
		L_9 = VirtualFuncInvoker0< int32_t >::Invoke(9 /* System.Int32 System.IO.TextReader::Peek() */, L_8);
		if ((!(((uint32_t)L_9) == ((uint32_t)(-1)))))
		{
			goto IL_0060;
		}
	}
	{
		// parsing = false;
		V_2 = (bool)0;
		// break;
		goto IL_0139;
	}

IL_0060:
	{
		// c = NextChar;
		Il2CppChar L_10;
		L_10 = Parser_get_NextChar_mAADE98129462252CD3EE9FD09976F3CCA541E004(__this, NULL);
		V_1 = L_10;
		Il2CppChar L_11 = V_1;
		if ((!(((uint32_t)L_11) <= ((uint32_t)((int32_t)92)))))
		{
			goto IL_0080;
		}
	}
	{
		Il2CppChar L_12 = V_1;
		if ((((int32_t)L_12) == ((int32_t)((int32_t)34))))
		{
			goto IL_00b7;
		}
	}
	{
		Il2CppChar L_13 = V_1;
		if ((((int32_t)L_13) == ((int32_t)((int32_t)47))))
		{
			goto IL_00b7;
		}
	}
	{
		Il2CppChar L_14 = V_1;
		if ((((int32_t)L_14) == ((int32_t)((int32_t)92))))
		{
			goto IL_00b7;
		}
	}
	{
		goto IL_0139;
	}

IL_0080:
	{
		Il2CppChar L_15 = V_1;
		if ((!(((uint32_t)L_15) <= ((uint32_t)((int32_t)102)))))
		{
			goto IL_0094;
		}
	}
	{
		Il2CppChar L_16 = V_1;
		if ((((int32_t)L_16) == ((int32_t)((int32_t)98))))
		{
			goto IL_00c1;
		}
	}
	{
		Il2CppChar L_17 = V_1;
		if ((((int32_t)L_17) == ((int32_t)((int32_t)102))))
		{
			goto IL_00cb;
		}
	}
	{
		goto IL_0139;
	}

IL_0094:
	{
		Il2CppChar L_18 = V_1;
		if ((((int32_t)L_18) == ((int32_t)((int32_t)110))))
		{
			goto IL_00d6;
		}
	}
	{
		Il2CppChar L_19 = V_1;
		switch (((int32_t)il2cpp_codegen_subtract((int32_t)L_19, ((int32_t)114))))
		{
			case 0:
			{
				goto IL_00e1;
			}
			case 1:
			{
				goto IL_0139;
			}
			case 2:
			{
				goto IL_00ec;
			}
			case 3:
			{
				goto IL_00f7;
			}
		}
	}
	{
		goto IL_0139;
	}

IL_00b7:
	{
		// s.Append(c);
		StringBuilder_t* L_20 = V_0;
		Il2CppChar L_21 = V_1;
		NullCheck(L_20);
		StringBuilder_t* L_22;
		L_22 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_20, L_21, NULL);
		// break;
		goto IL_0139;
	}

IL_00c1:
	{
		// s.Append('\b');
		StringBuilder_t* L_23 = V_0;
		NullCheck(L_23);
		StringBuilder_t* L_24;
		L_24 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_23, 8, NULL);
		// break;
		goto IL_0139;
	}

IL_00cb:
	{
		// s.Append('\f');
		StringBuilder_t* L_25 = V_0;
		NullCheck(L_25);
		StringBuilder_t* L_26;
		L_26 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_25, ((int32_t)12), NULL);
		// break;
		goto IL_0139;
	}

IL_00d6:
	{
		// s.Append('\n');
		StringBuilder_t* L_27 = V_0;
		NullCheck(L_27);
		StringBuilder_t* L_28;
		L_28 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_27, ((int32_t)10), NULL);
		// break;
		goto IL_0139;
	}

IL_00e1:
	{
		// s.Append('\r');
		StringBuilder_t* L_29 = V_0;
		NullCheck(L_29);
		StringBuilder_t* L_30;
		L_30 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_29, ((int32_t)13), NULL);
		// break;
		goto IL_0139;
	}

IL_00ec:
	{
		// s.Append('\t');
		StringBuilder_t* L_31 = V_0;
		NullCheck(L_31);
		StringBuilder_t* L_32;
		L_32 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_31, ((int32_t)9), NULL);
		// break;
		goto IL_0139;
	}

IL_00f7:
	{
		// var hex = new StringBuilder();
		StringBuilder_t* L_33 = (StringBuilder_t*)il2cpp_codegen_object_new(StringBuilder_t_il2cpp_TypeInfo_var);
		NullCheck(L_33);
		StringBuilder__ctor_m1D99713357DE05DAFA296633639DB55F8C30587D(L_33, NULL);
		V_3 = L_33;
		// for (int i=0; i< 4; i++) {
		V_4 = 0;
		goto IL_0115;
	}

IL_0102:
	{
		// hex.Append(NextChar);
		StringBuilder_t* L_34 = V_3;
		Il2CppChar L_35;
		L_35 = Parser_get_NextChar_mAADE98129462252CD3EE9FD09976F3CCA541E004(__this, NULL);
		NullCheck(L_34);
		StringBuilder_t* L_36;
		L_36 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_34, L_35, NULL);
		// for (int i=0; i< 4; i++) {
		int32_t L_37 = V_4;
		V_4 = ((int32_t)il2cpp_codegen_add(L_37, 1));
	}

IL_0115:
	{
		// for (int i=0; i< 4; i++) {
		int32_t L_38 = V_4;
		if ((((int32_t)L_38) < ((int32_t)4)))
		{
			goto IL_0102;
		}
	}
	{
		// s.Append((char) Convert.ToInt32(hex.ToString(), 16));
		StringBuilder_t* L_39 = V_0;
		StringBuilder_t* L_40 = V_3;
		NullCheck(L_40);
		String_t* L_41;
		L_41 = VirtualFuncInvoker0< String_t* >::Invoke(3 /* System.String System.Object::ToString() */, L_40);
		il2cpp_codegen_runtime_class_init_inline(Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var);
		int32_t L_42;
		L_42 = Convert_ToInt32_mD1B3AFBDA26E52D0382434804364FEF8BA241FB4(L_41, ((int32_t)16), NULL);
		NullCheck(L_39);
		StringBuilder_t* L_43;
		L_43 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_39, ((int32_t)(uint16_t)L_42), NULL);
		// break;
		goto IL_0139;
	}

IL_0131:
	{
		// s.Append(c);
		StringBuilder_t* L_44 = V_0;
		Il2CppChar L_45 = V_1;
		NullCheck(L_44);
		StringBuilder_t* L_46;
		L_46 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_44, L_45, NULL);
	}

IL_0139:
	{
		// while (parsing) {
		bool L_47 = V_2;
		if (L_47)
		{
			goto IL_0019;
		}
	}

IL_013f:
	{
		// return s.ToString();
		StringBuilder_t* L_48 = V_0;
		NullCheck(L_48);
		String_t* L_49;
		L_49 = VirtualFuncInvoker0< String_t* >::Invoke(3 /* System.String System.Object::ToString() */, L_48);
		return L_49;
	}
}
// System.Object AmplitudeNS.MiniJSON.Json/Parser::ParseNumber()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Parser_ParseNumber_mBF13A88AE39CA73AF9D56CAE6E4D32830D6F3236 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	String_t* V_0 = NULL;
	{
		// string number = NextWord;
		String_t* L_0;
		L_0 = Parser_get_NextWord_mFBD96068AF0333AD478C0F61F66EFFE77286E6A6(__this, NULL);
		V_0 = L_0;
		// if (number.IndexOf('.') == -1) {
		String_t* L_1 = V_0;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = String_IndexOf_mE21E78F35EF4A7768E385A72814C88D22B689966(L_1, ((int32_t)46), NULL);
		if ((!(((uint32_t)L_2) == ((uint32_t)(-1)))))
		{
			goto IL_001e;
		}
	}
	{
		// return int.Parse(number);
		String_t* L_3 = V_0;
		int32_t L_4;
		L_4 = Int32_Parse_m273CA1A9C7717C99641291A95C543711C0202AF0(L_3, NULL);
		int32_t L_5 = L_4;
		RuntimeObject* L_6 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_5);
		return L_6;
	}

IL_001e:
	{
		// return float.Parse(number);
		String_t* L_7 = V_0;
		float L_8;
		L_8 = Single_Parse_m621F610BB84997A2E3C4686913F482316CD3E6B8(L_7, NULL);
		float L_9 = L_8;
		RuntimeObject* L_10 = Box(Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, &L_9);
		return L_10;
	}
}
// System.Void AmplitudeNS.MiniJSON.Json/Parser::EatWhitespace()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Parser_EatWhitespace_mE52A290A5EC4A836EBE0401812A550BED86EA908 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralEBC658B067B5C785A3F0BB67D73755F6FEE7F70C);
		s_Il2CppMethodInitialized = true;
	}
	{
		goto IL_001c;
	}

IL_0002:
	{
		// json.Read();
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_0 = __this->___json_2;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = VirtualFuncInvoker0< int32_t >::Invoke(10 /* System.Int32 System.IO.TextReader::Read() */, L_0);
		// if (json.Peek() == -1) {
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_2 = __this->___json_2;
		NullCheck(L_2);
		int32_t L_3;
		L_3 = VirtualFuncInvoker0< int32_t >::Invoke(9 /* System.Int32 System.IO.TextReader::Peek() */, L_2);
		if ((((int32_t)L_3) == ((int32_t)(-1))))
		{
			goto IL_002f;
		}
	}

IL_001c:
	{
		// while (WHITE_SPACE.IndexOf(PeekChar) != -1) {
		Il2CppChar L_4;
		L_4 = Parser_get_PeekChar_mCB4B296524971AEAAE42C8F25B008196E5F41E8E(__this, NULL);
		NullCheck(_stringLiteralEBC658B067B5C785A3F0BB67D73755F6FEE7F70C);
		int32_t L_5;
		L_5 = String_IndexOf_mE21E78F35EF4A7768E385A72814C88D22B689966(_stringLiteralEBC658B067B5C785A3F0BB67D73755F6FEE7F70C, L_4, NULL);
		if ((!(((uint32_t)L_5) == ((uint32_t)(-1)))))
		{
			goto IL_0002;
		}
	}

IL_002f:
	{
		// }
		return;
	}
}
// System.Char AmplitudeNS.MiniJSON.Json/Parser::get_PeekChar()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar Parser_get_PeekChar_mCB4B296524971AEAAE42C8F25B008196E5F41E8E (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return Convert.ToChar(json.Peek());
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_0 = __this->___json_2;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = VirtualFuncInvoker0< int32_t >::Invoke(9 /* System.Int32 System.IO.TextReader::Peek() */, L_0);
		il2cpp_codegen_runtime_class_init_inline(Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var);
		Il2CppChar L_2;
		L_2 = Convert_ToChar_mF1B1B205DDEFDE52251235514E7DAFCAB37D1F24(L_1, NULL);
		return L_2;
	}
}
// System.Char AmplitudeNS.MiniJSON.Json/Parser::get_NextChar()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar Parser_get_NextChar_mAADE98129462252CD3EE9FD09976F3CCA541E004 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return Convert.ToChar(json.Read());
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_0 = __this->___json_2;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = VirtualFuncInvoker0< int32_t >::Invoke(10 /* System.Int32 System.IO.TextReader::Read() */, L_0);
		il2cpp_codegen_runtime_class_init_inline(Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var);
		Il2CppChar L_2;
		L_2 = Convert_ToChar_mF1B1B205DDEFDE52251235514E7DAFCAB37D1F24(L_1, NULL);
		return L_2;
	}
}
// System.String AmplitudeNS.MiniJSON.Json/Parser::get_NextWord()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Parser_get_NextWord_mFBD96068AF0333AD478C0F61F66EFFE77286E6A6 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringBuilder_t_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF468E0BCDE9855E7830073A32AF7323CC7E46952);
		s_Il2CppMethodInitialized = true;
	}
	StringBuilder_t* V_0 = NULL;
	{
		// StringBuilder word = new StringBuilder();
		StringBuilder_t* L_0 = (StringBuilder_t*)il2cpp_codegen_object_new(StringBuilder_t_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		StringBuilder__ctor_m1D99713357DE05DAFA296633639DB55F8C30587D(L_0, NULL);
		V_0 = L_0;
		goto IL_0023;
	}

IL_0008:
	{
		// word.Append(NextChar);
		StringBuilder_t* L_1 = V_0;
		Il2CppChar L_2;
		L_2 = Parser_get_NextChar_mAADE98129462252CD3EE9FD09976F3CCA541E004(__this, NULL);
		NullCheck(L_1);
		StringBuilder_t* L_3;
		L_3 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_1, L_2, NULL);
		// if (json.Peek() == -1) {
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_4 = __this->___json_2;
		NullCheck(L_4);
		int32_t L_5;
		L_5 = VirtualFuncInvoker0< int32_t >::Invoke(9 /* System.Int32 System.IO.TextReader::Peek() */, L_4);
		if ((((int32_t)L_5) == ((int32_t)(-1))))
		{
			goto IL_0036;
		}
	}

IL_0023:
	{
		// while (WORD_BREAK.IndexOf(PeekChar) == -1) {
		Il2CppChar L_6;
		L_6 = Parser_get_PeekChar_mCB4B296524971AEAAE42C8F25B008196E5F41E8E(__this, NULL);
		NullCheck(_stringLiteralF468E0BCDE9855E7830073A32AF7323CC7E46952);
		int32_t L_7;
		L_7 = String_IndexOf_mE21E78F35EF4A7768E385A72814C88D22B689966(_stringLiteralF468E0BCDE9855E7830073A32AF7323CC7E46952, L_6, NULL);
		if ((((int32_t)L_7) == ((int32_t)(-1))))
		{
			goto IL_0008;
		}
	}

IL_0036:
	{
		// return word.ToString();
		StringBuilder_t* L_8 = V_0;
		NullCheck(L_8);
		String_t* L_9;
		L_9 = VirtualFuncInvoker0< String_t* >::Invoke(3 /* System.String System.Object::ToString() */, L_8);
		return L_9;
	}
}
// AmplitudeNS.MiniJSON.Json/Parser/TOKEN AmplitudeNS.MiniJSON.Json/Parser::get_NextToken()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Parser_get_NextToken_mADD25509957CAF9025E0928F68E86271637203A3 (Parser_t5CE444F52863545C1D883D8E083F9F5C67124951* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5BEFD8CC60A79699B5BB00E37BAC5B62D371E174);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral77D38C0623F92B292B925F6E72CF5CF99A20D4EB);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB7C45DD316C68ABF3429C20058C2981C652192F2);
		s_Il2CppMethodInitialized = true;
	}
	Il2CppChar V_0 = 0x0;
	String_t* V_1 = NULL;
	{
		// EatWhitespace();
		Parser_EatWhitespace_mE52A290A5EC4A836EBE0401812A550BED86EA908(__this, NULL);
		// if (json.Peek() == -1) {
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_0 = __this->___json_2;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = VirtualFuncInvoker0< int32_t >::Invoke(9 /* System.Int32 System.IO.TextReader::Peek() */, L_0);
		if ((!(((uint32_t)L_1) == ((uint32_t)(-1)))))
		{
			goto IL_0016;
		}
	}
	{
		// return TOKEN.NONE;
		return (int32_t)(0);
	}

IL_0016:
	{
		// char c = PeekChar;
		Il2CppChar L_2;
		L_2 = Parser_get_PeekChar_mCB4B296524971AEAAE42C8F25B008196E5F41E8E(__this, NULL);
		V_0 = L_2;
		Il2CppChar L_3 = V_0;
		if ((!(((uint32_t)L_3) <= ((uint32_t)((int32_t)91)))))
		{
			goto IL_0096;
		}
	}
	{
		Il2CppChar L_4 = V_0;
		switch (((int32_t)il2cpp_codegen_subtract((int32_t)L_4, ((int32_t)34))))
		{
			case 0:
			{
				goto IL_00d5;
			}
			case 1:
			{
				goto IL_00db;
			}
			case 2:
			{
				goto IL_00db;
			}
			case 3:
			{
				goto IL_00db;
			}
			case 4:
			{
				goto IL_00db;
			}
			case 5:
			{
				goto IL_00db;
			}
			case 6:
			{
				goto IL_00db;
			}
			case 7:
			{
				goto IL_00db;
			}
			case 8:
			{
				goto IL_00db;
			}
			case 9:
			{
				goto IL_00db;
			}
			case 10:
			{
				goto IL_00c7;
			}
			case 11:
			{
				goto IL_00d9;
			}
			case 12:
			{
				goto IL_00db;
			}
			case 13:
			{
				goto IL_00db;
			}
			case 14:
			{
				goto IL_00d9;
			}
			case 15:
			{
				goto IL_00d9;
			}
			case 16:
			{
				goto IL_00d9;
			}
			case 17:
			{
				goto IL_00d9;
			}
			case 18:
			{
				goto IL_00d9;
			}
			case 19:
			{
				goto IL_00d9;
			}
			case 20:
			{
				goto IL_00d9;
			}
			case 21:
			{
				goto IL_00d9;
			}
			case 22:
			{
				goto IL_00d9;
			}
			case 23:
			{
				goto IL_00d9;
			}
			case 24:
			{
				goto IL_00d7;
			}
		}
	}
	{
		Il2CppChar L_5 = V_0;
		if ((((int32_t)L_5) == ((int32_t)((int32_t)91))))
		{
			goto IL_00b7;
		}
	}
	{
		goto IL_00db;
	}

IL_0096:
	{
		Il2CppChar L_6 = V_0;
		if ((((int32_t)L_6) == ((int32_t)((int32_t)93))))
		{
			goto IL_00b9;
		}
	}
	{
		Il2CppChar L_7 = V_0;
		if ((((int32_t)L_7) == ((int32_t)((int32_t)123))))
		{
			goto IL_00a7;
		}
	}
	{
		Il2CppChar L_8 = V_0;
		if ((((int32_t)L_8) == ((int32_t)((int32_t)125))))
		{
			goto IL_00a9;
		}
	}
	{
		goto IL_00db;
	}

IL_00a7:
	{
		// return TOKEN.CURLY_OPEN;
		return (int32_t)(1);
	}

IL_00a9:
	{
		// json.Read();
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_9 = __this->___json_2;
		NullCheck(L_9);
		int32_t L_10;
		L_10 = VirtualFuncInvoker0< int32_t >::Invoke(10 /* System.Int32 System.IO.TextReader::Read() */, L_9);
		// return TOKEN.CURLY_CLOSE;
		return (int32_t)(2);
	}

IL_00b7:
	{
		// return TOKEN.SQUARED_OPEN;
		return (int32_t)(3);
	}

IL_00b9:
	{
		// json.Read();
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_11 = __this->___json_2;
		NullCheck(L_11);
		int32_t L_12;
		L_12 = VirtualFuncInvoker0< int32_t >::Invoke(10 /* System.Int32 System.IO.TextReader::Read() */, L_11);
		// return TOKEN.SQUARED_CLOSE;
		return (int32_t)(4);
	}

IL_00c7:
	{
		// json.Read();
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_13 = __this->___json_2;
		NullCheck(L_13);
		int32_t L_14;
		L_14 = VirtualFuncInvoker0< int32_t >::Invoke(10 /* System.Int32 System.IO.TextReader::Read() */, L_13);
		// return TOKEN.COMMA;
		return (int32_t)(6);
	}

IL_00d5:
	{
		// return TOKEN.STRING;
		return (int32_t)(7);
	}

IL_00d7:
	{
		// return TOKEN.COLON;
		return (int32_t)(5);
	}

IL_00d9:
	{
		// return TOKEN.NUMBER;
		return (int32_t)(8);
	}

IL_00db:
	{
		// string word = NextWord;
		String_t* L_15;
		L_15 = Parser_get_NextWord_mFBD96068AF0333AD478C0F61F66EFFE77286E6A6(__this, NULL);
		V_1 = L_15;
		String_t* L_16 = V_1;
		bool L_17;
		L_17 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_16, _stringLiteral77D38C0623F92B292B925F6E72CF5CF99A20D4EB, NULL);
		if (L_17)
		{
			goto IL_010b;
		}
	}
	{
		String_t* L_18 = V_1;
		bool L_19;
		L_19 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_18, _stringLiteralB7C45DD316C68ABF3429C20058C2981C652192F2, NULL);
		if (L_19)
		{
			goto IL_010e;
		}
	}
	{
		String_t* L_20 = V_1;
		bool L_21;
		L_21 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_20, _stringLiteral5BEFD8CC60A79699B5BB00E37BAC5B62D371E174, NULL);
		if (L_21)
		{
			goto IL_0111;
		}
	}
	{
		goto IL_0114;
	}

IL_010b:
	{
		// return TOKEN.FALSE;
		return (int32_t)(((int32_t)10));
	}

IL_010e:
	{
		// return TOKEN.TRUE;
		return (int32_t)(((int32_t)9));
	}

IL_0111:
	{
		// return TOKEN.NULL;
		return (int32_t)(((int32_t)11));
	}

IL_0114:
	{
		// return TOKEN.NONE;
		return (int32_t)(0);
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void AmplitudeNS.MiniJSON.Json/Serializer::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Serializer__ctor_m40CBCABFB74A8C1E82DF0F678EDC65806A765A63 (Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringBuilder_t_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Serializer() {
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		// builder = new StringBuilder();
		StringBuilder_t* L_0 = (StringBuilder_t*)il2cpp_codegen_object_new(StringBuilder_t_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		StringBuilder__ctor_m1D99713357DE05DAFA296633639DB55F8C30587D(L_0, NULL);
		__this->___builder_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___builder_0), (void*)L_0);
		// }
		return;
	}
}
// System.String AmplitudeNS.MiniJSON.Json/Serializer::Serialize(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Serializer_Serialize_mF754FA742B06F31D9855F2522EE45FEE35BBDAD8 (RuntimeObject* ___obj0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// var instance = new Serializer();
		Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328* L_0 = (Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328*)il2cpp_codegen_object_new(Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		Serializer__ctor_m40CBCABFB74A8C1E82DF0F678EDC65806A765A63(L_0, NULL);
		// instance.SerializeValue(obj);
		Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328* L_1 = L_0;
		RuntimeObject* L_2 = ___obj0;
		NullCheck(L_1);
		Serializer_SerializeValue_m4FBEC42A2539CE9F469367033AAFEFEAB2FA7551(L_1, L_2, NULL);
		// return instance.builder.ToString();
		NullCheck(L_1);
		StringBuilder_t* L_3 = L_1->___builder_0;
		NullCheck(L_3);
		String_t* L_4;
		L_4 = VirtualFuncInvoker0< String_t* >::Invoke(3 /* System.String System.Object::ToString() */, L_3);
		return L_4;
	}
}
// System.Void AmplitudeNS.MiniJSON.Json/Serializer::SerializeValue(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Serializer_SerializeValue_m4FBEC42A2539CE9F469367033AAFEFEAB2FA7551 (Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328* __this, RuntimeObject* ___value0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IList_t1C522956D79B7DC92B5B01053DF1AC058C8B598D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&String_t_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5BEFD8CC60A79699B5BB00E37BAC5B62D371E174);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	RuntimeObject* V_1 = NULL;
	String_t* V_2 = NULL;
	{
		// if (value == null) {
		RuntimeObject* L_0 = ___value0;
		if (L_0)
		{
			goto IL_0015;
		}
	}
	{
		// builder.Append("null");
		StringBuilder_t* L_1 = __this->___builder_0;
		NullCheck(L_1);
		StringBuilder_t* L_2;
		L_2 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_1, _stringLiteral5BEFD8CC60A79699B5BB00E37BAC5B62D371E174, NULL);
		return;
	}

IL_0015:
	{
		// else if ((asStr = value as string) != null) {
		RuntimeObject* L_3 = ___value0;
		String_t* L_4 = ((String_t*)IsInstSealed((RuntimeObject*)L_3, String_t_il2cpp_TypeInfo_var));
		V_2 = L_4;
		if (!L_4)
		{
			goto IL_0027;
		}
	}
	{
		// SerializeString(asStr);
		String_t* L_5 = V_2;
		Serializer_SerializeString_m90926F2D28A47048AFE41C71647E1B38CB24864A(__this, L_5, NULL);
		return;
	}

IL_0027:
	{
		// else if (value is bool) {
		RuntimeObject* L_6 = ___value0;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_6, Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var)))
		{
			goto IL_0047;
		}
	}
	{
		// builder.Append(value.ToString().ToLower());
		StringBuilder_t* L_7 = __this->___builder_0;
		RuntimeObject* L_8 = ___value0;
		NullCheck(L_8);
		String_t* L_9;
		L_9 = VirtualFuncInvoker0< String_t* >::Invoke(3 /* System.String System.Object::ToString() */, L_8);
		NullCheck(L_9);
		String_t* L_10;
		L_10 = String_ToLower_m6191ABA3DC514ED47C10BDA23FD0DDCEAE7ACFBD(L_9, NULL);
		NullCheck(L_7);
		StringBuilder_t* L_11;
		L_11 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_7, L_10, NULL);
		return;
	}

IL_0047:
	{
		// else if ((asList = value as IList) != null) {
		RuntimeObject* L_12 = ___value0;
		RuntimeObject* L_13 = ((RuntimeObject*)IsInst((RuntimeObject*)L_12, IList_t1C522956D79B7DC92B5B01053DF1AC058C8B598D_il2cpp_TypeInfo_var));
		V_0 = L_13;
		if (!L_13)
		{
			goto IL_0059;
		}
	}
	{
		// SerializeArray(asList);
		RuntimeObject* L_14 = V_0;
		Serializer_SerializeArray_m9743D1022222CEBEDAEBEAF42AFDCF3619E63991(__this, L_14, NULL);
		return;
	}

IL_0059:
	{
		// else if ((asDict = value as IDictionary) != null) {
		RuntimeObject* L_15 = ___value0;
		RuntimeObject* L_16 = ((RuntimeObject*)IsInst((RuntimeObject*)L_15, IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220_il2cpp_TypeInfo_var));
		V_1 = L_16;
		if (!L_16)
		{
			goto IL_006b;
		}
	}
	{
		// SerializeObject(asDict);
		RuntimeObject* L_17 = V_1;
		Serializer_SerializeObject_mF46ED6A4B7CF7BDBC5971C2E5302C77B37EE024D(__this, L_17, NULL);
		return;
	}

IL_006b:
	{
		// else if (value is char) {
		RuntimeObject* L_18 = ___value0;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_18, Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var)))
		{
			goto IL_0080;
		}
	}
	{
		// SerializeString(value.ToString());
		RuntimeObject* L_19 = ___value0;
		NullCheck(L_19);
		String_t* L_20;
		L_20 = VirtualFuncInvoker0< String_t* >::Invoke(3 /* System.String System.Object::ToString() */, L_19);
		Serializer_SerializeString_m90926F2D28A47048AFE41C71647E1B38CB24864A(__this, L_20, NULL);
		return;
	}

IL_0080:
	{
		// SerializeOther(value);
		RuntimeObject* L_21 = ___value0;
		Serializer_SerializeOther_m512C9EBB20ED31EBC8692C238E9292C2FE996BFF(__this, L_21, NULL);
		// }
		return;
	}
}
// System.Void AmplitudeNS.MiniJSON.Json/Serializer::SerializeObject(System.Collections.IDictionary)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Serializer_SerializeObject_mF46ED6A4B7CF7BDBC5971C2E5302C77B37EE024D (Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328* __this, RuntimeObject* ___obj0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerable_t6331596D5DD37C462B1B8D49CF6B319B00AB7131_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	RuntimeObject* V_1 = NULL;
	RuntimeObject* V_2 = NULL;
	RuntimeObject* V_3 = NULL;
	{
		// bool first = true;
		V_0 = (bool)1;
		// builder.Append('{');
		StringBuilder_t* L_0 = __this->___builder_0;
		NullCheck(L_0);
		StringBuilder_t* L_1;
		L_1 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_0, ((int32_t)123), NULL);
		// foreach (object e in obj.Keys) {
		RuntimeObject* L_2 = ___obj0;
		NullCheck(L_2);
		RuntimeObject* L_3;
		L_3 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(2 /* System.Collections.ICollection System.Collections.IDictionary::get_Keys() */, IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220_il2cpp_TypeInfo_var, L_2);
		NullCheck(L_3);
		RuntimeObject* L_4;
		L_4 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* System.Collections.IEnumerator System.Collections.IEnumerable::GetEnumerator() */, IEnumerable_t6331596D5DD37C462B1B8D49CF6B319B00AB7131_il2cpp_TypeInfo_var, L_3);
		V_1 = L_4;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0069:
			{// begin finally (depth: 1)
				{
					RuntimeObject* L_5 = V_1;
					V_3 = ((RuntimeObject*)IsInst((RuntimeObject*)L_5, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var));
					RuntimeObject* L_6 = V_3;
					if (!L_6)
					{
						goto IL_0079;
					}
				}
				{
					RuntimeObject* L_7 = V_3;
					NullCheck(L_7);
					InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_7);
				}

IL_0079:
				{
					return;
				}
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_005f_1;
			}

IL_001e_1:
			{
				// foreach (object e in obj.Keys) {
				RuntimeObject* L_8 = V_1;
				NullCheck(L_8);
				RuntimeObject* L_9;
				L_9 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(1 /* System.Object System.Collections.IEnumerator::get_Current() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_8);
				V_2 = L_9;
				// if (!first) {
				bool L_10 = V_0;
				if (L_10)
				{
					goto IL_0036_1;
				}
			}
			{
				// builder.Append(',');
				StringBuilder_t* L_11 = __this->___builder_0;
				NullCheck(L_11);
				StringBuilder_t* L_12;
				L_12 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_11, ((int32_t)44), NULL);
			}

IL_0036_1:
			{
				// SerializeString(e.ToString());
				RuntimeObject* L_13 = V_2;
				NullCheck(L_13);
				String_t* L_14;
				L_14 = VirtualFuncInvoker0< String_t* >::Invoke(3 /* System.String System.Object::ToString() */, L_13);
				Serializer_SerializeString_m90926F2D28A47048AFE41C71647E1B38CB24864A(__this, L_14, NULL);
				// builder.Append(':');
				StringBuilder_t* L_15 = __this->___builder_0;
				NullCheck(L_15);
				StringBuilder_t* L_16;
				L_16 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_15, ((int32_t)58), NULL);
				// SerializeValue(obj[e]);
				RuntimeObject* L_17 = ___obj0;
				RuntimeObject* L_18 = V_2;
				NullCheck(L_17);
				RuntimeObject* L_19;
				L_19 = InterfaceFuncInvoker1< RuntimeObject*, RuntimeObject* >::Invoke(0 /* System.Object System.Collections.IDictionary::get_Item(System.Object) */, IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220_il2cpp_TypeInfo_var, L_17, L_18);
				Serializer_SerializeValue_m4FBEC42A2539CE9F469367033AAFEFEAB2FA7551(__this, L_19, NULL);
				// first = false;
				V_0 = (bool)0;
			}

IL_005f_1:
			{
				// foreach (object e in obj.Keys) {
				RuntimeObject* L_20 = V_1;
				NullCheck(L_20);
				bool L_21;
				L_21 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_20);
				if (L_21)
				{
					goto IL_001e_1;
				}
			}
			{
				goto IL_007a;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_007a:
	{
		// builder.Append('}');
		StringBuilder_t* L_22 = __this->___builder_0;
		NullCheck(L_22);
		StringBuilder_t* L_23;
		L_23 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_22, ((int32_t)125), NULL);
		// }
		return;
	}
}
// System.Void AmplitudeNS.MiniJSON.Json/Serializer::SerializeArray(System.Collections.IList)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Serializer_SerializeArray_m9743D1022222CEBEDAEBEAF42AFDCF3619E63991 (Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328* __this, RuntimeObject* ___anArray0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerable_t6331596D5DD37C462B1B8D49CF6B319B00AB7131_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	RuntimeObject* V_1 = NULL;
	RuntimeObject* V_2 = NULL;
	RuntimeObject* V_3 = NULL;
	{
		// builder.Append('[');
		StringBuilder_t* L_0 = __this->___builder_0;
		NullCheck(L_0);
		StringBuilder_t* L_1;
		L_1 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_0, ((int32_t)91), NULL);
		// bool first = true;
		V_0 = (bool)1;
		// foreach (object obj in anArray) {
		RuntimeObject* L_2 = ___anArray0;
		NullCheck(L_2);
		RuntimeObject* L_3;
		L_3 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* System.Collections.IEnumerator System.Collections.IEnumerable::GetEnumerator() */, IEnumerable_t6331596D5DD37C462B1B8D49CF6B319B00AB7131_il2cpp_TypeInfo_var, L_2);
		V_1 = L_3;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0044:
			{// begin finally (depth: 1)
				{
					RuntimeObject* L_4 = V_1;
					V_3 = ((RuntimeObject*)IsInst((RuntimeObject*)L_4, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var));
					RuntimeObject* L_5 = V_3;
					if (!L_5)
					{
						goto IL_0054;
					}
				}
				{
					RuntimeObject* L_6 = V_3;
					NullCheck(L_6);
					InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_6);
				}

IL_0054:
				{
					return;
				}
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_003a_1;
			}

IL_0019_1:
			{
				// foreach (object obj in anArray) {
				RuntimeObject* L_7 = V_1;
				NullCheck(L_7);
				RuntimeObject* L_8;
				L_8 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(1 /* System.Object System.Collections.IEnumerator::get_Current() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_7);
				V_2 = L_8;
				// if (!first) {
				bool L_9 = V_0;
				if (L_9)
				{
					goto IL_0031_1;
				}
			}
			{
				// builder.Append(',');
				StringBuilder_t* L_10 = __this->___builder_0;
				NullCheck(L_10);
				StringBuilder_t* L_11;
				L_11 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_10, ((int32_t)44), NULL);
			}

IL_0031_1:
			{
				// SerializeValue(obj);
				RuntimeObject* L_12 = V_2;
				Serializer_SerializeValue_m4FBEC42A2539CE9F469367033AAFEFEAB2FA7551(__this, L_12, NULL);
				// first = false;
				V_0 = (bool)0;
			}

IL_003a_1:
			{
				// foreach (object obj in anArray) {
				RuntimeObject* L_13 = V_1;
				NullCheck(L_13);
				bool L_14;
				L_14 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_13);
				if (L_14)
				{
					goto IL_0019_1;
				}
			}
			{
				goto IL_0055;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0055:
	{
		// builder.Append(']');
		StringBuilder_t* L_15 = __this->___builder_0;
		NullCheck(L_15);
		StringBuilder_t* L_16;
		L_16 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_15, ((int32_t)93), NULL);
		// }
		return;
	}
}
// System.Void AmplitudeNS.MiniJSON.Json/Serializer::SerializeString(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Serializer_SerializeString_m90926F2D28A47048AFE41C71647E1B38CB24864A (Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328* __this, String_t* ___str0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5962E944D7340CE47999BF097B4AFD70C1501FB9);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral785F17F45C331C415D0A7458E6AAC36966399C51);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7F3238CD8C342B06FB9AB185C610175C84625462);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral848E5ED630B3142F565DD995C6E8D30187ED33CD);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralA7C3FCA8C63E127B542B38A5CA5E3FEEDDD1B122);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB78F235D4291950A7D101307609C259F3E1F033F);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDA666908BB15F4E1D2649752EC5DCBD0D5C64699);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF18840F490E42D3CE48CDCBF47229C1C240F8ABE);
		s_Il2CppMethodInitialized = true;
	}
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* V_0 = NULL;
	int32_t V_1 = 0;
	Il2CppChar V_2 = 0x0;
	int32_t V_3 = 0;
	{
		// builder.Append('\"');
		StringBuilder_t* L_0 = __this->___builder_0;
		NullCheck(L_0);
		StringBuilder_t* L_1;
		L_1 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_0, ((int32_t)34), NULL);
		// char[] charArray = str.ToCharArray();
		String_t* L_2 = ___str0;
		NullCheck(L_2);
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_3;
		L_3 = String_ToCharArray_m0699A92AA3E744229EF29CB9D943C47DF4FE5B46(L_2, NULL);
		// foreach (var c in charArray) {
		V_0 = L_3;
		V_1 = 0;
		goto IL_0127;
	}

IL_001c:
	{
		// foreach (var c in charArray) {
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_4 = V_0;
		int32_t L_5 = V_1;
		NullCheck(L_4);
		int32_t L_6 = L_5;
		uint16_t L_7 = (uint16_t)(L_4)->GetAt(static_cast<il2cpp_array_size_t>(L_6));
		V_2 = L_7;
		Il2CppChar L_8 = V_2;
		switch (((int32_t)il2cpp_codegen_subtract((int32_t)L_8, 8)))
		{
			case 0:
			{
				goto IL_007b;
			}
			case 1:
			{
				goto IL_00ca;
			}
			case 2:
			{
				goto IL_00a4;
			}
			case 3:
			{
				goto IL_00dd;
			}
			case 4:
			{
				goto IL_0091;
			}
			case 5:
			{
				goto IL_00b7;
			}
		}
	}
	{
		Il2CppChar L_9 = V_2;
		if ((((int32_t)L_9) == ((int32_t)((int32_t)34))))
		{
			goto IL_004f;
		}
	}
	{
		Il2CppChar L_10 = V_2;
		if ((((int32_t)L_10) == ((int32_t)((int32_t)92))))
		{
			goto IL_0065;
		}
	}
	{
		goto IL_00dd;
	}

IL_004f:
	{
		// builder.Append("\\\"");
		StringBuilder_t* L_11 = __this->___builder_0;
		NullCheck(L_11);
		StringBuilder_t* L_12;
		L_12 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_11, _stringLiteral848E5ED630B3142F565DD995C6E8D30187ED33CD, NULL);
		// break;
		goto IL_0123;
	}

IL_0065:
	{
		// builder.Append("\\\\");
		StringBuilder_t* L_13 = __this->___builder_0;
		NullCheck(L_13);
		StringBuilder_t* L_14;
		L_14 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_13, _stringLiteralF18840F490E42D3CE48CDCBF47229C1C240F8ABE, NULL);
		// break;
		goto IL_0123;
	}

IL_007b:
	{
		// builder.Append("\\b");
		StringBuilder_t* L_15 = __this->___builder_0;
		NullCheck(L_15);
		StringBuilder_t* L_16;
		L_16 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_15, _stringLiteral5962E944D7340CE47999BF097B4AFD70C1501FB9, NULL);
		// break;
		goto IL_0123;
	}

IL_0091:
	{
		// builder.Append("\\f");
		StringBuilder_t* L_17 = __this->___builder_0;
		NullCheck(L_17);
		StringBuilder_t* L_18;
		L_18 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_17, _stringLiteralA7C3FCA8C63E127B542B38A5CA5E3FEEDDD1B122, NULL);
		// break;
		goto IL_0123;
	}

IL_00a4:
	{
		// builder.Append("\\n");
		StringBuilder_t* L_19 = __this->___builder_0;
		NullCheck(L_19);
		StringBuilder_t* L_20;
		L_20 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_19, _stringLiteral785F17F45C331C415D0A7458E6AAC36966399C51, NULL);
		// break;
		goto IL_0123;
	}

IL_00b7:
	{
		// builder.Append("\\r");
		StringBuilder_t* L_21 = __this->___builder_0;
		NullCheck(L_21);
		StringBuilder_t* L_22;
		L_22 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_21, _stringLiteralB78F235D4291950A7D101307609C259F3E1F033F, NULL);
		// break;
		goto IL_0123;
	}

IL_00ca:
	{
		// builder.Append("\\t");
		StringBuilder_t* L_23 = __this->___builder_0;
		NullCheck(L_23);
		StringBuilder_t* L_24;
		L_24 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_23, _stringLiteral7F3238CD8C342B06FB9AB185C610175C84625462, NULL);
		// break;
		goto IL_0123;
	}

IL_00dd:
	{
		// int codepoint = Convert.ToInt32(c);
		Il2CppChar L_25 = V_2;
		il2cpp_codegen_runtime_class_init_inline(Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var);
		int32_t L_26;
		L_26 = Convert_ToInt32_mDBBE9318A7CCE1560974CE93F5BFED9931CF0052(L_25, NULL);
		V_3 = L_26;
		// if ((codepoint >= 32) && (codepoint <= 126)) {
		int32_t L_27 = V_3;
		if ((((int32_t)L_27) < ((int32_t)((int32_t)32))))
		{
			goto IL_00fd;
		}
	}
	{
		int32_t L_28 = V_3;
		if ((((int32_t)L_28) > ((int32_t)((int32_t)126))))
		{
			goto IL_00fd;
		}
	}
	{
		// builder.Append(c);
		StringBuilder_t* L_29 = __this->___builder_0;
		Il2CppChar L_30 = V_2;
		NullCheck(L_29);
		StringBuilder_t* L_31;
		L_31 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_29, L_30, NULL);
		goto IL_0123;
	}

IL_00fd:
	{
		// builder.Append("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
		StringBuilder_t* L_32 = __this->___builder_0;
		int32_t L_33 = V_3;
		il2cpp_codegen_runtime_class_init_inline(Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var);
		String_t* L_34;
		L_34 = Convert_ToString_mC3349029FE37EB00B5BFCB1F87022458A3834E35(L_33, ((int32_t)16), NULL);
		NullCheck(L_34);
		String_t* L_35;
		L_35 = String_PadLeft_m99DDD242908E78B71E9631EE66331E8A130EB31F(L_34, 4, ((int32_t)48), NULL);
		String_t* L_36;
		L_36 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteralDA666908BB15F4E1D2649752EC5DCBD0D5C64699, L_35, NULL);
		NullCheck(L_32);
		StringBuilder_t* L_37;
		L_37 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_32, L_36, NULL);
	}

IL_0123:
	{
		int32_t L_38 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_38, 1));
	}

IL_0127:
	{
		// foreach (var c in charArray) {
		int32_t L_39 = V_1;
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_40 = V_0;
		NullCheck(L_40);
		if ((((int32_t)L_39) < ((int32_t)((int32_t)(((RuntimeArray*)L_40)->max_length)))))
		{
			goto IL_001c;
		}
	}
	{
		// builder.Append('\"');
		StringBuilder_t* L_41 = __this->___builder_0;
		NullCheck(L_41);
		StringBuilder_t* L_42;
		L_42 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_41, ((int32_t)34), NULL);
		// }
		return;
	}
}
// System.Void AmplitudeNS.MiniJSON.Json/Serializer::SerializeOther(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Serializer_SerializeOther_m512C9EBB20ED31EBC8692C238E9292C2FE996BFF (Serializer_tF120AF52036BF13BF24F652B8CE55B8FE2868328* __this, RuntimeObject* ___value0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UInt16_tF4C148C876015C212FD72652D0B6ED8CC247A455_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UInt32_t1833D51FFA667B18A5AA4B8D34DE284F8495D29B_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UInt64_t8F12534CC8FC4B5860F2A2CD1EE79D322E7A41AF_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	double V_1 = 0.0;
	{
		// if (value is float) {
		RuntimeObject* L_0 = ___value0;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var)))
		{
			goto IL_0028;
		}
	}
	{
		// builder.Append(((float) value).ToString(CultureInfo.InvariantCulture));
		StringBuilder_t* L_1 = __this->___builder_0;
		RuntimeObject* L_2 = ___value0;
		V_0 = ((*(float*)((float*)(float*)UnBox(L_2, Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var))));
		il2cpp_codegen_runtime_class_init_inline(CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_il2cpp_TypeInfo_var);
		CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* L_3;
		L_3 = CultureInfo_get_InvariantCulture_mD1E96DC845E34B10F78CB744B0CB5D7D63CEB1E6(NULL);
		String_t* L_4;
		L_4 = Single_ToString_m534852BD7949AA972435783D7B96D0FFB09F6D6A((&V_0), L_3, NULL);
		NullCheck(L_1);
		StringBuilder_t* L_5;
		L_5 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_1, L_4, NULL);
		return;
	}

IL_0028:
	{
		// } else if (value is int
		//     || value is uint
		//     || value is long
		//     || value is sbyte
		//     || value is byte
		//     || value is short
		//     || value is ushort
		//     || value is ulong) {
		RuntimeObject* L_6 = ___value0;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_6, Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var)))
		{
			goto IL_0068;
		}
	}
	{
		RuntimeObject* L_7 = ___value0;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_7, UInt32_t1833D51FFA667B18A5AA4B8D34DE284F8495D29B_il2cpp_TypeInfo_var)))
		{
			goto IL_0068;
		}
	}
	{
		RuntimeObject* L_8 = ___value0;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_8, Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var)))
		{
			goto IL_0068;
		}
	}
	{
		RuntimeObject* L_9 = ___value0;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_9, SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var)))
		{
			goto IL_0068;
		}
	}
	{
		RuntimeObject* L_10 = ___value0;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_10, Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var)))
		{
			goto IL_0068;
		}
	}
	{
		RuntimeObject* L_11 = ___value0;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_11, Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var)))
		{
			goto IL_0068;
		}
	}
	{
		RuntimeObject* L_12 = ___value0;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_12, UInt16_tF4C148C876015C212FD72652D0B6ED8CC247A455_il2cpp_TypeInfo_var)))
		{
			goto IL_0068;
		}
	}
	{
		RuntimeObject* L_13 = ___value0;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_13, UInt64_t8F12534CC8FC4B5860F2A2CD1EE79D322E7A41AF_il2cpp_TypeInfo_var)))
		{
			goto IL_007b;
		}
	}

IL_0068:
	{
		// builder.Append(value.ToString());
		StringBuilder_t* L_14 = __this->___builder_0;
		RuntimeObject* L_15 = ___value0;
		NullCheck(L_15);
		String_t* L_16;
		L_16 = VirtualFuncInvoker0< String_t* >::Invoke(3 /* System.String System.Object::ToString() */, L_15);
		NullCheck(L_14);
		StringBuilder_t* L_17;
		L_17 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_14, L_16, NULL);
		return;
	}

IL_007b:
	{
		// } else if (value is double
		//     || value is decimal) {
		RuntimeObject* L_18 = ___value0;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_18, Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var)))
		{
			goto IL_008b;
		}
	}
	{
		RuntimeObject* L_19 = ___value0;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_19, Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var)))
		{
			goto IL_00ab;
		}
	}

IL_008b:
	{
		// builder.Append(Convert.ToDouble(value).ToString(CultureInfo.InvariantCulture));
		StringBuilder_t* L_20 = __this->___builder_0;
		RuntimeObject* L_21 = ___value0;
		il2cpp_codegen_runtime_class_init_inline(Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var);
		double L_22;
		L_22 = Convert_ToDouble_m86FF4F837721833186E883102C056A35F0860EB0(L_21, NULL);
		V_1 = L_22;
		il2cpp_codegen_runtime_class_init_inline(CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_il2cpp_TypeInfo_var);
		CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* L_23;
		L_23 = CultureInfo_get_InvariantCulture_mD1E96DC845E34B10F78CB744B0CB5D7D63CEB1E6(NULL);
		String_t* L_24;
		L_24 = Double_ToString_m4318830D9F771852FDCF21C14CF9E8ABC7E77357((&V_1), L_23, NULL);
		NullCheck(L_20);
		StringBuilder_t* L_25;
		L_25 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_20, L_24, NULL);
		return;
	}

IL_00ab:
	{
		// SerializeString(value.ToString());
		RuntimeObject* L_26 = ___value0;
		NullCheck(L_26);
		String_t* L_27;
		L_27 = VirtualFuncInvoker0< String_t* >::Invoke(3 /* System.String System.Object::ToString() */, L_26);
		Serializer_SerializeString_m90926F2D28A47048AFE41C71647E1B38CB24864A(__this, L_27, NULL);
		// }
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_gshared_inline (const RuntimeMethod* method) 
{
	{
		il2cpp_codegen_runtime_class_init_inline(il2cpp_rgctx_data(method->rgctx_data, 0));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ((EmptyArray_1_tDF0DD7256B115243AA6BD5558417387A734240EE_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value_0;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___item0, const RuntimeMethod* method) 
{
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = (int32_t)__this->____version_3;
		__this->____version_3 = ((int32_t)il2cpp_codegen_add(L_0, 1));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)__this->____items_1;
		V_0 = L_1;
		int32_t L_2 = (int32_t)__this->____size_2;
		V_1 = L_2;
		int32_t L_3 = V_1;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = V_0;
		NullCheck(L_4);
		if ((!(((uint32_t)L_3) < ((uint32_t)((int32_t)(((RuntimeArray*)L_4)->max_length))))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_5 = V_1;
		__this->____size_2 = ((int32_t)il2cpp_codegen_add(L_5, 1));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = V_0;
		int32_t L_7 = V_1;
		RuntimeObject* L_8 = ___item0;
		NullCheck(L_6);
		(L_6)->SetAt(static_cast<il2cpp_array_size_t>(L_7), (RuntimeObject*)L_8);
		return;
	}

IL_0034:
	{
		RuntimeObject* L_9 = ___item0;
		((  void (*) (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*, RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 11)))(__this, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 11));
		return;
	}
}
