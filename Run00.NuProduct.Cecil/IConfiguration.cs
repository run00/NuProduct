﻿
namespace Run00.NuProduct.Cecil
{
	//TODO: Replace this with more production worthy configuration
	public interface IConfiguration
	{
		string Get(string key);
		void Set(string key, string value);
	}
}
