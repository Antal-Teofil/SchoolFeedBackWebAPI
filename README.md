# SchoolFeedBackWebAPI
This is the main project repository

==========================================================================================================================================================================================================================================================
`develop` branch:
  -ezen az agon tesztelheto verziok lesznek feltoltve, olyan verziok, amelyek mindenki reszerol mukodnek es az app egysegeben tesztelheto.

A `develop` branch-bol letrehoztam mindenki szamara egy a nevevel ellatott kulon branch-t. Mindenki erre a sajat branchre fogja pusholni azt amin jelenleg dolgozik.
Amennyiben szukseg van valaki mas munkajara, abban az esetben valaki indit egy `feature` tipusu branch-t a sajat nevevel ellatott branchbol. A `feature` branch neve legyen beszedes, egyertelmu.
Ebben a letrehozott feature branch-be merge-lodnek a kozos munkak. A feature branchbol, tovabba letrehozhatoak `fix` tipusu brancheket, amelyeken lokalis bug-fixeket vegezhetunk.
Amikor keszen vagyunk a kozos munkaval a `fix` tipusu brancheken, akkor amennyiben mukodik es a felek megegyzetek, mergelheto a kozos `feature` branchbe. Amennyiben megvolt a code-review mergelodhetnek a `develop` branchbe.

Amikor egy funkcionalisan tesztelheto alkalmazasig eljutottunk akkor letrehoz valaki egy `Release #number` tipusu brancht, ez kotelezoen a `develop` agbol fog szarmazni. Amikor mindenki levalidalta a kozos munkat, akkor felkerulhet `Release #number`.

FONTOS!!!

Mielott kikerulne egy `Release #number` egy COMMENTS.md fileban mindenki nev szerint odairja a javaslatat vagy a megjegyzeseit. A commentekbol kiderulo hibakat, mindig a `develop` agon csinaljuk.
==========================================================================================================================================================================================================================================================
