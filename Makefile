default: native

native:
	g++ -shared -fPIC -o ./bin/native.o ./native/c.cc
	ld -G ./bin/*.o -o ./bin/native.so

managed:
	mono-csc -lib:./ -r:Raven.Abstractions.dll -out:./bin/test.exe ./test/native.cs ./test/program.cs ./test/db.cs ./test/readstream.cs
	cp ./test/*.config ./bin/
	cp ./*.dll ./bin/

test: native managed
	./bin/test.exe
