#pragma once

#include <wtypes.h>
#include <windows.h>
#include <Oleauto.h>
extern "C" {
	//Edit by AJS Team from www.lisp.vn
	__declspec(dllexport) int VnConvertFile(int inCharset, int outCharset, const char* inFile, const char* outFile);
	_declspec(dllexport) BSTR VnConvertString(int inCharset, int outCharset, BYTE* input, int inlen);
	__declspec(dllexport) int VnConvertOut(int inCharset, int outCharset, BYTE* input, BYTE* output, int inlen, int outlen);
	__declspec(dllexport) int VnConvert0(int inCharset, int outCharset, BYTE* input, BYTE* output, int* pInLen, int* pMaxOutLen);
}

