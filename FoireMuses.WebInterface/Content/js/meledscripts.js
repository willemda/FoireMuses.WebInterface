// Melody editor
// By Michael Schierl, Rainer Typke and Vincent DARON

var keyboardLayouts = {
    de: "01234qwertzuiopüasdfghjklöäQWERTZUIOPÜASDFGHJKLÖÄx56789",
    us: "01234qwertyuiop[asdfghjkl;]QWERTYUIOP{ASDFGHJKL:}x56789",
    fr: "à&é\"'azertyuiop^qsdfghjklmùAZERTYUIOP¨QSDFGHJKLM%x(-è_ç"
};

var notes = 
    [{digits: "00", flats: "00",  name: "r",      keyindex: [0]},
     {digits: "88", flats: "88",  name: "c",      keyindex: []},
     {digits: "89", flats: "89b", name: "cis",    keyindex: []},
	 {digits: "89", flats: "89b", name: "ces",    keyindex: []},
     {digits: "90", flats: "90",  name: "d",      keyindex: []},
     {digits: "91", flats: "91b", name: "dis",    keyindex: []},
     {digits: "92", flats: "92",  name: "e",      keyindex: []},
     {digits: "93", flats: "93",  name: "f",      keyindex: []},
     {digits: "94", flats: "94b", name: "fis",    keyindex: []},
	 {digits: "94", flats: "94b", name: "fes",    keyindex: []},
     {digits: "95", flats: "95",  name: "g",      keyindex: []},
     {digits: "96", flats: "96b", name: "gis",    keyindex: []},
	 {digits: "96", flats: "96b", name: "ges",    keyindex: []},
     {digits: "97", flats: "97",  name: "a",      keyindex: []},
     {digits: "98", flats: "98b", name: "ais",    keyindex: []},
	 {digits: "98", flats: "98b", name: "aes",    keyindex: []},
     {digits: "99", flats: "99",  name: "b",      keyindex: []},
     {digits: "01", flats: "01",  name: "c'",     keyindex: [16]},
     {digits: "02", flats: "02b", name: "cis'",   keyindex: [6]},
	 {digits: "02", flats: "02b", name: "ces'",   keyindex: [6]},
     {digits: "03", flats: "03",  name: "d'",     keyindex: [17]},
     {digits: "04", flats: "04b", name: "dis'",   keyindex: [7]},
	 {digits: "04", flats: "04b", name: "des'",   keyindex: [7]},
     {digits: "05", flats: "05",  name: "e'",     keyindex: [18]},
     {digits: "06", flats: "06",  name: "f'",     keyindex: [19]},
     {digits: "07", flats: "07b", name: "fis'",   keyindex: [9]},
	 {digits: "04", flats: "04b", name: "fes'",   keyindex: [7]},
     {digits: "08", flats: "08",  name: "g'",     keyindex: [20]},
     {digits: "09", flats: "09b", name: "gis'",   keyindex: [10]},
	 {digits: "04", flats: "04b", name: "ges'",   keyindex: [7]},
     {digits: "10", flats: "10",  name: "a'",     keyindex: [21]},
     {digits: "11", flats: "11b", name: "ais'",   keyindex: [11]},
	 {digits: "04", flats: "04b", name: "aes'",   keyindex: [7]},
     {digits: "12", flats: "12",  name: "b'",     keyindex: [22]},
     {digits: "13", flats: "13",  name: "c''",    keyindex: [23,38]},
     {digits: "14", flats: "14b", name: "cis''",  keyindex: [13, 28]},
	 {digits: "14", flats: "14b", name: "ces''",  keyindex: [13, 28]},
     {digits: "15", flats: "15",  name: "d''",    keyindex: [24, 39]},
     {digits: "16", flats: "16b", name: "dis''",  keyindex: [14, 29]},
	 {digits: "16", flats: "16b", name: "des''",  keyindex: [14, 29]},
     {digits: "17", flats: "17",  name: "e''",    keyindex: [25, 40]},
     {digits: "18", flats: "18",  name: "f''",    keyindex: [26, 41]},
     {digits: "19", flats: "19b", name: "fis''",  keyindex: [31]},
	 {digits: "19", flats: "19b", name: "fes''",  keyindex: [31]},
     {digits: "20", flats: "20",  name: "g''",    keyindex: [42]},
     {digits: "21", flats: "21b", name: "gis''",  keyindex: [32]},
	 {digits: "19", flats: "19b", name: "ges''",  keyindex: [31]},
     {digits: "22", flats: "22",  name: "a''",    keyindex: [43]},
     {digits: "23", flats: "23b", name: "ais''",  keyindex: [33]},
	 {digits: "19", flats: "19b", name: "aes''",  keyindex: [31]},
     {digits: "24", flats: "24",  name: "b''",    keyindex: [44]},
     {digits: "25", flats: "25",  name: "c'''",   keyindex: []},
     {digits: "26", flats: "26b", name: "cis'''", keyindex: []},
	 {digits: "26", flats: "26b", name: "ces'''", keyindex: []},
     {digits: "27", flats: "27",  name: "d'''",   keyindex: []},
     {digits: "28", flats: "28b", name: "dis'''", keyindex: []},
	 {digits: "26", flats: "26b", name: "des'''", keyindex: []},
     {digits: "29", flats: "29",  name: "e'''",   keyindex: []},
     {digits: "30", flats: "30",  name: "f'''",   keyindex: []},
     {digits: "31", flats: "31b", name: "fis'''", keyindex: []},
	 {digits: "26", flats: "26b", name: "fes'''", keyindex: []}
	 
     ];

var lengths = ["1", "2.", "2", "4.", "4", "8.", "8", "16", "32"];
var delays = [2000, 1500, 1000, 750, 500, 375, 250, 125, 62];



var current = [], aftercursor=[];
var kbd = keyboardLayouts.us;
var url = document.location.href;
var flats = false;
var playsound = false;

function setlayout() {
    kbd = keyboardLayouts[document.getElementById("layout").value];
}

function handleNote(note, add) {
    var idx = kbd.indexOf(note);
    if (kbd == -1) return false;
    for(var i=0; i< notes.length; i++) {
        for(var j=0; j< notes[i].keyindex.length; j++) {
            if(notes[i].keyindex[j] == idx) {
                if (add) {
                    addNote(i, 1);
                } else {
                    playNote(i)
                }
                return true;
            }
        }
    }
    return false;
}

function playNote(n) {
    soundManager.play(n);
}

function addNote(n, l) {
	if(playsound)
	    playNote(n);
	if (l.substring(l.length - 1) == ".") {
	    current.push({ n: n, l: l.substring(0,l.length - 1), p: true });
	} else {
	    current.push({ n: n, l: l, p: false });
	}
    updateView();
}

function delayFromLength(aLength){
	for(var i = 0; i<lengths.length;i++){
		if(lengths[i] == aLength){
			return delays[i];
		}
	}
	return null;
}

function deleteNote() {
    if (current.length > 0) current.pop();
    updateView();
}

function updateView() {
    var s="", spos=0;
    var c = '<img src="../../Content/images/piano_js/clef.gif" onclick="setCursor(0);" />';
    for(var i=0; i<current.length ;i++) {
	c+=noteImage(current[i], i+1);
	s+=noteText(current[i]);
	if(i!=(current.length-1)||aftercursor.length>0)
		s+=" ";
    }
    c += '<img src="../../Content/images/piano_js/cursor.gif" />';
    for(var i=0;i<aftercursor.length;i++) {
	c+=noteImage(aftercursor[i], i+1+current.length);
	s+=noteText(aftercursor[i]);
	if (i != (aftercursor.length - 1))
		s+=" ";
    }
    document.getElementById("current").innerHTML=c;
    document.getElementById("music").value=s;
}

function noteText(note) {
    if(note.l < 0)
	return note.name;
    return note.n+note.l+ (note.p?".":"");
}

function noteImage(note, pos) {
    if(note.l < 0)
        return '<img src="../../Content/images/piano_js/00_0.gif" onclick="setCursor(' + pos + ');" />';
    return '<img src="../../Content/images/piano_js/' + note.n + '' + note.l + (note.p?"p":"")+'.gif" onclick="setCursor(' + pos + ');" />';
}

function handleKey(keychar) {
    if (keychar == ' ') {
        if (current.length>0) {
            var nn = current[current.length-1];
            if (nn.l<4) {
                nn.l++;
            } else {
                play(0, 1);
            }
        }
    } else if (keychar == kbd[49]) {
        deleteNote();
    } else if (keychar == kbd[1]) {
        setLength(1);
    } else if (keychar == kbd[2]) {
        setLength(2);
    } else if (keychar == kbd[3]) {
        setLength(3);
    } else if (keychar == kbd[4]) {
        setLength(4);
    } else if (keychar == kbd[50]) {
        setLength(5);
    } else if (keychar == kbd[51]) {
        setLength(6);
    } else if (keychar == kbd[52]) {
        setLength(7);
    } else if (keychar == kbd[53]) {
        setLength(8);
    } else if (keychar == kbd[54]) {
        setLength(9);
    }
    updateView();
}

function setLength(len) {
    var pointee = len.substring(len.length - 1) == ".";
    if (pointee) {
        current[current.length - 1].l = len.substring(0,len.length - 1);
        current[current.length - 1].p = true;
    } else {
        current[current.length - 1].l = len
        current[current.length - 1].p = false;
    }
    updateView();
}

var playing = [];

function playAll() {
    if(playing.length==0) {
        for(var i=0; i<current.length;i++) {
            playing.push(current[i]);
        }
        for(var i=0; i<aftercursor.length; i++) {
            playing.push(aftercursor[i]);
        }
        playArray();
    } else {
        alert("Still playing!");
    }
}

function playArray() {
    if (playing.length==0) return;
    var n = playing.shift();
    if (n.l<=0) {
        playArray();
    } else {
        playNote(n.n);
        setTimeout(playArray, delayFromLength(n.l));
    }
}

function parse(e) {
    var touche = e.keyCode ? e.keyCode : e.charCode
    var nom = String.fromCharCode(touche);
    if (touche != 32) {
        var newnotes = [];
        var nn = document.getElementById("music").value.split(/ /g);
        for (var i = 0; i < nn.length; i++) {
            var nomOriginal = nn[i];
            if (nomOriginal == "")
                continue;
            var found = 0;
            for (var nt = 0; nt < notes.length; nt++) {
                var nname = notes[nt].name;
                var pointee = nomOriginal.substring(nomOriginal.length - 1) == ".";
                var nb = 0;
                if (pointee) {
                    nb++;
                }
                var match = nomOriginal.match(/\d+/);
                if (match === null) {
                    break;
                }
                var entier = parseInt(match[0], 10);
                if (entier > 0 && entier <= 9) {
                    nb += 1
                } else if (entier > 9) {
                    nb += 2;
                }
                var nomACheck = nomOriginal;
                if (nb > 0) {
                    nomACheck = nomOriginal.substring(0, nomOriginal.length - nb);
                }
                if (nomACheck == nname) {
                    for (var len = 1; len <= lengths.length; len++) {
                        if (nomOriginal == nname + lengths[len - 1]) {
                            if (pointee)
                                newnotes.push({ n: nname, l: entier, p: true });
                            else
                                newnotes.push({ n: nname, l: entier, p: false });
                            found = 1;
                            break;
                        }
                    }
                }
                if (found == 1) break;
            }
            // Add unknown note
            if (found == 0) {
                newnotes.push({ n: -1, l: -1, name: nomOriginal });
            }
        }
        aftercursor = [];
        current = newnotes;
        updateView();
    }
}

function setCursor(newpos) {
    var toadd = newpos - current.length;
    for(var i=0; i<toadd; i++) {
        current.push(aftercursor.shift());
    }
    for(var i=0; i<-toadd; i++) {
        aftercursor.unshift(current.pop());
    }
    updateView();
}

function shiftNote(diff) {
    if(current.length > 0) {
        var nn = current[current.length-1];
        if (nn.n == 0) return;
        nn.n += diff;
        if (nn.n<1) nn.n=1;
        if (nn.n>43) nn.n=43;
        playNote(nn.n);
        updateView();
    }
}

function toggleFlats() {
    flats = !flats
	var reg;
	var replace;
	if(flats){
		reg = new RegExp("(is)","g");
		replace = "es";
	}else{
		reg = new RegExp("(es)","g");
		replace = "is";
	}
	for(var i=0; i<current.length; i++) {
        current[i].n=current[i].n.replace(reg,replace);
    }
    for(var i=0; i<aftercursor.length; i++) {
        aftercursor[i].n=aftercursor[i].n.replace(reg,replace);
    }
    updateView();
}

function toggleSound(elem) {
    playsound = !playsound;
    var imageName = "soundoff.png";
    if(playsound)
      imageName = "soundon.png";
    $(elem).html("<img src=\"../../Content/images/" + imageName + "\">");
}

function cssReplaceClass(oldClass, newClass) {
    cssReplaceClass2(document, oldClass, newClass);
}

function cssReplaceClass2(node, oldClass, newClass) {
    if (node.className == oldClass) {
        node.className = newClass;
    }
    for(var i=0; i<node.childNodes.length; i++) {
	cssReplaceClass2(node.childNodes[i], oldClass, newClass);
    }
}

function mousemode() {
    cssReplaceClass("mouseplay", "mouseplayvisible");
    cssReplaceClass("keyboardplayvisible", "keyboardplay");
}

function keyboardmode() {
    cssReplaceClass("keyboardplay", "keyboardplayvisible");
    cssReplaceClass("mouseplayvisible", "mouseplay");
    document.getElementById("real").focus();    
}

	// initialization code
	function playmusic_init() {
	
		// Add available keyboard layouts to list
		var kb = '<select name="layout" id="layout" onchange="setlayout()">';
		for(var k in keyboardLayouts) {
		    kb+='<option'+ (kbd==keyboardLayouts[k] ? ' selected="selected"':'') +'>'+k+'</option>';
		}
		kb+='</select>';
		//document.getElementById("kbdlayout").innerHTML=kb;

		updateView();
		//soundManagerInit();
		//enable_search();
	}

