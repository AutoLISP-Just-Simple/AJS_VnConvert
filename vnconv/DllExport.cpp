#include "stdafx.h"
#include "vnconv.h"
#include "charset.h"
#include "DllExport.h"
#include <string>

std::string ConvertWideToANSI(const std::wstring& wstr)
{
	int count = WideCharToMultiByte(CP_ACP, 0, wstr.c_str(), wstr.length(), NULL, 0, NULL, NULL);
	std::string str(count, 0);
	WideCharToMultiByte(CP_ACP, 0, wstr.c_str(), -1, &str[0], count, NULL, NULL);
	return str;
}

std::wstring ConvertAnsiToWide(const std::string& str, int count)
{
	count = MultiByteToWideChar(CP_ACP, 0, str.c_str(), count, NULL, 0);
	std::wstring wstr(count, 0);
	MultiByteToWideChar(CP_ACP, 0, str.c_str(), str.length(), &wstr[0], count);
	return wstr;
}

std::wstring ConvertAnsiToWide(const std::string& str)
{
	int count = MultiByteToWideChar(CP_ACP, 0, str.c_str(), str.length(), NULL, 0);
	std::wstring wstr(count, 0);
	MultiByteToWideChar(CP_ACP, 0, str.c_str(), str.length(), &wstr[0], count);
	return wstr;
}

std::string ConvertWideToUtf8(const std::wstring& wstr)
{
	int count = WideCharToMultiByte(CP_UTF8, 0, wstr.c_str(), wstr.length(), NULL, 0, NULL, NULL);
	std::string str(count, 0);
	WideCharToMultiByte(CP_UTF8, 0, wstr.c_str(), -1, &str[0], count, NULL, NULL);
	return str;
}

std::wstring ConvertUtf8ToWide(const std::string& str)
{
	int count = MultiByteToWideChar(CP_UTF8, 0, str.c_str(), str.length(), NULL, 0);
	std::wstring wstr(count, 0);
	MultiByteToWideChar(CP_UTF8, 0, str.c_str(), str.length(), &wstr[0], count);
	return wstr;
}

std::wstring ConvertUtf8ToWide(const std::string& str, int count)
{
	count = MultiByteToWideChar(CP_UTF8, 0, str.c_str(), count, NULL, 0);
	std::wstring wstr(count, 0);
	MultiByteToWideChar(CP_UTF8, 0, str.c_str(), str.length(), &wstr[0], count);
	return wstr;
}

extern "C" {
	_declspec(dllexport) BSTR VnConvertString(int inCharset, int outCharset, BYTE* input, int inlen)
	{
		int pInLen = inlen;
		int pMaxOutLen = 3000;
		BYTE* output = new unsigned char[pMaxOutLen];
		int ret = VnConvert0(inCharset, outCharset, input, output, &inlen, &pMaxOutLen);

		char const* outchar = reinterpret_cast<char const*>(output);
		//std::string str(output);
		std::string sName(reinterpret_cast<char*>(output));
		std::wstring wstr = outCharset <= 1 ? ConvertUtf8ToWide(sName, pMaxOutLen) : ConvertAnsiToWide(sName, pMaxOutLen);
		//int wslen = MultiByteToWideChar(outCharset <= 1 ? CP_UTF8 : CP_ACP, 0, sName.c_str(), pMaxOutLen, 0, 0);
		return SysAllocString(wstr.c_str());
	}

	__declspec(dllexport) int VnConvertOut(int inCharset, int outCharset, BYTE* input, BYTE* output, int inlen, int outlen)
	{
		int pInLen = inlen;
		int pMaxOutLen = outlen;
		return VnConvert0(inCharset, outCharset, input, output, &pInLen, &pMaxOutLen);
	}
	__declspec(dllexport) int VnConvert0(int inCharset, int outCharset, BYTE* input, BYTE* output, int* pInLen, int* pMaxOutLen)
	{
		int inLen, maxOutLen;
		int ret = -1;

		inLen = *pInLen;
		maxOutLen = *pMaxOutLen;

		if (inLen != -1 && inLen < 0) // invalid inLen
			return ret;

		VnCharset* pInCharset = VnCharsetLibObj.getVnCharset(inCharset);
		VnCharset* pOutCharset = VnCharsetLibObj.getVnCharset(outCharset);

		if (!pInCharset || !pOutCharset)
			return VNCONV_INVALID_CHARSET;

		StringBIStream is(input, inLen);
		StringBOStream os(output, maxOutLen);

		ret = genConvert(*pInCharset, *pOutCharset, is, os);
		*pMaxOutLen = os.getOutBytes();
		*pInLen = is.left();
		return ret;
	}

	__declspec(dllexport) int VnConvertFile(int inCharset, int outCharset, const char* inFile, const char* outFile)
	{
		return VnFileConvert(inCharset, outCharset, inFile, outFile);
	}

	//Edit by AJS Team from www.lisp.vn
}