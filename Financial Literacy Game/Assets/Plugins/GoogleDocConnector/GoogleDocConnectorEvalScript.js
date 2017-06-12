
function EvalAssignValue(targetScript : String, targetVariable : String, targetValue : Object) {

	var target = GetComponent(targetScript);
	
	if (target != null) {
		try {
		
			#if !UNITY_IPHONE
			eval("target." + targetVariable + " = targetValue;");
			#endif
			return true;
		} catch (err) {
			// Missed field exception catch is here
			return false;
		}
	}
	
	return false;
}
