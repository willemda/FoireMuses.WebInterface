soundManager.debugMode = true; 
soundManager.debugFlash = true;
soundManager.url = '~/Content/soundmanager/soundmanager2_debug.swf';
soundManager.onerror = function() {
  // SM2 could not start, no sound support, something broke etc. Handle gracefully.
  alert('error while loading sound player');
}
soundManager.onload = function() { 
  soundManager.createSound({id:'c\'', url:'../../Content/mp3/notes/c1_8.mp3'});
  soundManager.createSound({id:'c\'\'', url:'../../Content/mp3/notes/c2_8.mp3'});
  soundManager.createSound({id:'cis\'', url:'../../Content/mp3/notes/cis1_8.mp3'});
  soundManager.createSound({id:'cis\'\'', url:'../../Content/mp3/notes/cis2_8.mp3'});
  soundManager.createSound({id:'d\'', url:'../../Content/mp3/notes/d1_8.mp3'});
  soundManager.createSound({id:'d\'\'', url:'../../Content/mp3/notes/d2_8.mp3'});
  soundManager.createSound({id:'dis\'', url:'../../Content/mp3/notes/dis1_8.mp3'});
  soundManager.createSound({id:'dis\'\'', url:'../../Content/mp3/notes/dis2_8.mp3'});
  soundManager.createSound({id:'e\'', url:'../../Content/mp3/notes/e1_8.mp3'});
  soundManager.createSound({id:'e\'\'', url:'../../Content/mp3/notes/e2_8.mp3'});
  soundManager.createSound({id:'f\'', url:'../../Content/mp3/notes/f1_8.mp3'});
  soundManager.createSound({id:'f\'\'', url:'../../Content/mp3/notes/f2_8.mp3'});
  soundManager.createSound({id:'fis\'', url:'../../Content/mp3/notes/fis1_8.mp3'});
  soundManager.createSound({id:'fis\'\'', url:'../../Content/mp3/notes/fis2_8.mp3'});
  soundManager.createSound({id:'g\'', url:'../../Content/mp3/notes/g1_8.mp3'});
  soundManager.createSound({id:'g\'\'', url:'../../Content/mp3/notes/g2_8.mp3'});
  soundManager.createSound({id:'gis\'', url:'../../Content/mp3/notes/gis1_8.mp3'});
  soundManager.createSound({id:'gis\'\'', url:'../../Content/mp3/notes/gis2_8.mp3'});
  soundManager.createSound({id:'a\'', url:'../../Content/mp3/notes/a1_8.mp3'});
  soundManager.createSound({id:'a\'\'', url:'../../Content/mp3/notes/a2_8.mp3'});
  soundManager.createSound({id:'ais\'\'', url:'../../Content/mp3/notes/ais1_8.mp3'});
  soundManager.createSound({id:'ais\'\'', url:'../../Content/mp3/notes/ais2_8.mp3'});
  soundManager.createSound({id:'b\'', url:'../../Content/mp3/notes/b1_8.mp3'});
  soundManager.createSound({id:'b\'\'', url:'../../Content/mp3/notes/b2_8.mp3'});
  soundManager.createSound({id:'c', url:'../../Content/mp3/notes/c0_8.mp3'});
  soundManager.createSound({id:'c\'\'\'', url:'../../Content/mp3/notes/c3_8.mp3'});
  soundManager.createSound({id:'cis', url:'../../Content/mp3/notes/cis0_8.mp3'});
  soundManager.createSound({id:'cis\'\'\'', url:'../../Content/mp3/notes/cis3_8.mp3'});
  soundManager.createSound({id:'d', url:'../../Content/mp3/notes/d0_8.mp3'});
  soundManager.createSound({id:'d\'\'\'', url:'../../Content/mp3/notes/d3_8.mp3'});
  soundManager.createSound({id:'dis', url:'../../Content/mp3/notes/dis0_8.mp3'});
  soundManager.createSound({id:'dis\'\'\'', url:'../../Content/mp3/notes/dis3_8.mp3'});
  soundManager.createSound({id:'e', url:'../../Content/mp3/notes/e0_8.mp3'});
  soundManager.createSound({id:'e\'\'\'', url:'../../Content/mp3/notes/e3_8.mp3'});
  soundManager.createSound({id:'f', url:'../../Content/mp3/notes/f0_8.mp3'});
  soundManager.createSound({id:'f\'\'\'', url:'../../Content/mp3/notes/f3_8.mp3'});
  soundManager.createSound({id:'fis', url:'../../Content/mp3/notes/fis0_8.mp3'});
  soundManager.createSound({id:'fis\'\'\'', url:'../../Content/mp3/notes/fis3_8.mp3'});
  soundManager.createSound({id:'g', url:'../../Content/mp3/notes/g0_8.mp3'});
  soundManager.createSound({id:'g\'\'\'', url:'../../Content/mp3/notes/g3_8.mp3'});
  soundManager.createSound({id:'gis', url:'../../Content/mp3/notes/gis0_8.mp3'});
  soundManager.createSound({id:'gis\'\'\'', url:'../../Content/mp3/notes/gis3_8.mp3'});
  soundManager.createSound({id:'a', url:'../../Content/mp3/notes/a0_8.mp3'});
  soundManager.createSound({id:'a\'\'\'', url:'../../Content/mp3/notes/a3_8.mp3'});
  soundManager.createSound({id:'ais', url:'../../Content/mp3/notes/ais0_8.mp3'});
  soundManager.createSound({id:'ais\'\'\'', url:'../../Content/mp3/notes/ais3_8.mp3'});
  soundManager.createSound({id:'b', url:'../../Content/mp3/notes/b0_8.mp3'});
  soundManager.createSound({id:'b\'\'\'', url:'../../Content/mp3/notes/b3_8.mp3'});
}
