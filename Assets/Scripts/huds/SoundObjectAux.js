#pragma strict
#pragma implicit
#pragma downcast

class SoundObjectAux extends MonoBehaviour
{
	public var soundGenerator : SoundObject;
	
    function Awake()
    {
        if(GetComponent.<Rigidbody>() != null) GetComponent.<Rigidbody>().Sleep();
    }

	function OnCollisionEnter(collision : Collision)
	{
		if(soundGenerator == null) Destroy(this);
		else soundGenerator.OnCollisionEnter(collision);
	}
}