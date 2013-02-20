default: native

native:
	g++ -shared -fPIC -o ./bin/native.o ./native/c.cc
	ld -G ./bin/*.o -o ./bin/native.so

managed:
	mono-csc -out:./bin/test.exe ./test/native.cs ./test/program.cs

test: managed
	./bin/test.exe
