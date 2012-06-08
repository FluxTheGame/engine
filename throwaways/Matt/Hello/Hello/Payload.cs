using Newtonsoft.Json;

namespace Hello
{
	[JsonObject(MemberSerialization.OptIn)]
	public class Payload
	{
		[JsonProperty]
		public string SomeDataOne { get; set; }

		[JsonProperty]
		public string SomeDataTwo { get; set; }

		[JsonProperty]
		public int SomethingElse { get; set; }

		public Payload()
		{
		}

		public string ToJsonString()
		{
			return JsonConvert.SerializeObject(this);
		}
		public static Payload Deserialize(string jsonString)
		{
			return JsonConvert.DeserializeObject<Payload>(jsonString);
		}
	}
}
