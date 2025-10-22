// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.46
// 

using Colyseus.Schema;

public partial class Player : Schema {
	[Type(0, "number")]
	public float x = default(float);

	[Type(1, "number")]
	public float y = default(float);

	[Type(2, "number")]
	public float z = default(float);

	[Type(3, "number")]
	public float velocityX = default(float);

	[Type(4, "number")]
	public float velocityY = default(float);

	[Type(5, "number")]
	public float velocityZ = default(float);

	[Type(6, "number")]
	public float bodyAngle = default(float);

	[Type(7, "array", typeof(ArraySchema<int>), "int32")]
	public ArraySchema<int> state = new ArraySchema<int>();
}

