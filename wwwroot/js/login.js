var passconf = document.getElementById(RegisterPasswordConfirm);
console.log("1");
passconf.onfocus = function () {
	document.getElementById("match").style.display = "block";
}
console.log("2");