#--------------------------------------------------
# Project:
# Purpose:
#--------------------------------------------------

.PHONY: all repl buid run

all: build

repl:
	@dotnet fsi

build:
	@dotnet build

run:
	@dotnet run
