#!/bin/bash
#WARNING! this script deletes a user named userxyz. Change the username here and in NpamTestsCommon.cs if you already have a user by that name on your system.
sudo userdel userxyz || true
sudo useradd -p $(perl -e 'print crypt($ARGV[0], "password")' "pwd123") userxyz