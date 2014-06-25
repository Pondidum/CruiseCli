require 'bundler/setup'
require 'albacore'

nugets_restore :restore do |n|
	n.exe = 'tools/nuget/nuget.exe'
	n.out = 'packages'
end

build :compile do |msb|
	msb.target = [ :clean, :rebuild ]
	msb.sln = 'CruiseCli.sln'
end

test_runner :test do |xunit|
	xunit.exe = 'tools/xunit/xunit.console.clr4.exe'
	xunit.files = FileList['**/bin/*/tests.dll']
end

task :default => [ :restore, :compile, :test ]
