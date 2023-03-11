project('girtest', 'c', version: '0.1')

gobject_dep = dependency('gobject-2.0', version: '>= 2.66.0')

header_files = [
  'girtest.h',
  'girtest-class-tester.h',
  'girtest-constant-tester.h',
  'girtest-error-tester.h',
  'girtest-primitive-value-type-tester.h',
  'girtest-property-tester.h',
  'girtest-returning-signal-tester.h',
  'girtest-string-tester.h',
]

source_files = [
  'girtest-class-tester.c',
  'girtest-error-tester.c',
  'girtest-primitive-value-type-tester.c',
  'girtest-property-tester.c',
  'girtest-returning-signal-tester.c',
  'girtest-string-tester.c',
]

# Build a shared library that depends on gobject.
lib = library('girtest',
  source_files,
  dependencies: [gobject_dep],
  install: true
)

# Generate the .gir file.
gnome = import('gnome')
gir = gnome.generate_gir(
  lib,
  sources: source_files + header_files,
  namespace: 'GirTest',
  nsversion: '0.1',
  identifier_prefix: 'GirTest',
  symbol_prefix: 'girtest',
  includes: ['GObject-2.0'],
  header: 'girtest.h',
  install_gir: true,
  fatal_warnings: true,
)
