#--------------------------------------------------
# Project:
# Purpose:
#--------------------------------------------------

.PHONY: all buid run

all: build

build:
	@dotnet build

run:
	@dotnet run
