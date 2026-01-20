set X=%1

mkdir old
copy logic_circuit_%X%_0.png old\logic_circuit_%X%_0.png
copy logic_circuit_%X%_build.bytes old\logic_circuit_%X%_build.bytes
copy logic_circuit_%X%_anim.bytes old\logic_circuit_%X%_anim.bytes
copy ui.png old\ui.png

kanimal scml --output ".\scml" logic_circuit_%X%_0.png logic_circuit_%X%_build.bytes logic_circuit_%X%_anim.bytes

del scml\ui_0.png
move ui.png scml\ui_0.png

del logic_circuit_%X%_0.png
del logic_circuit_%X%_build.bytes
del logic_circuit_%X%_anim.bytes

cd scml
kanimal kanim logic_circuit_%X%.scml --output ".."
cd ..
move logic_circuit_%X%.png logic_circuit_%X%_0.png
rd /s /q scml