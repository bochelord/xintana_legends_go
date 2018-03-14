using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_PokedexProfile : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		PokedexProfile data = (PokedexProfile)obj;
		// Add your writer.Write calls here.
		writer.Write(data.enemiesKnown);
		writer.Write(data.lastEnemyWorld);

	}
	
	public override object Read(ES2Reader reader)
	{
		PokedexProfile data = GetOrCreate<PokedexProfile>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		PokedexProfile data = (PokedexProfile)c;
		// Add your reader.Read calls here to read the data into the object.
		data.enemiesKnown = reader.ReadDictionary<EnemyType,System.Boolean>();
		data.lastEnemyWorld = reader.Read<System.Int32>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_PokedexProfile():base(typeof(PokedexProfile)){}
}