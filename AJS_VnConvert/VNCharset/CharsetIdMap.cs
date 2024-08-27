namespace AJS_VnConvert
{
    public class CharsetNameId
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public static class CharsetIdMap
    {
        public static readonly CharsetNameId[] CharsetIdMapArray = new CharsetNameId[]
        {
            new CharsetNameId { Name = "BKHCM1", Id = Charset.CONV_CHARSET_BKHCM1 },
            new CharsetNameId { Name = "BKHCM2", Id = Charset.CONV_CHARSET_BKHCM2 },
            new CharsetNameId { Name = "ISC", Id = Charset.CONV_CHARSET_ISC },
            new CharsetNameId { Name = "NCR-DEC", Id = Charset.CONV_CHARSET_UNIREF },
            new CharsetNameId { Name = "NCR-HEX", Id = Charset.CONV_CHARSET_UNIREF_HEX },
            new CharsetNameId { Name = "TCVN3", Id = Charset.CONV_CHARSET_TCVN3 },
            new CharsetNameId { Name = "UNI-COMP", Id = Charset.CONV_CHARSET_UNIDECOMPOSED },
            new CharsetNameId { Name = "UNICODE", Id = Charset.CONV_CHARSET_UNICODE },
            new CharsetNameId { Name = "UTF-8", Id = Charset.CONV_CHARSET_UNIUTF8 },
            new CharsetNameId { Name = "UTF8", Id = Charset.CONV_CHARSET_UNIUTF8 },
            new CharsetNameId { Name = "UVIQR", Id = Charset.CONV_CHARSET_UTF8VIQR },
            new CharsetNameId { Name = "VIETWARE-F", Id = Charset.CONV_CHARSET_VIETWAREF },
            new CharsetNameId { Name = "VIETWARE-X", Id = Charset.CONV_CHARSET_VIETWAREX },
            new CharsetNameId { Name = "VIQR", Id = Charset.CONV_CHARSET_VIQR },
            new CharsetNameId { Name = "VISCII", Id = Charset.CONV_CHARSET_VISCII },
            new CharsetNameId { Name = "VNI-MAC", Id = Charset.CONV_CHARSET_VNIMAC },
            new CharsetNameId { Name = "VNI-WIN", Id = Charset.CONV_CHARSET_VNIWIN },
            new CharsetNameId { Name = "VPS", Id = Charset.CONV_CHARSET_VPS },
            new CharsetNameId { Name = "WINCP-1258", Id = Charset.CONV_CHARSET_WINCP1258 }
        };
    }
}