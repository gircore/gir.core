project('girtest', 'c', version: '0.1')

gobject_dep = dependency('gobject-2.0', version: '>= 2.66.0')

header_files = [
  'girtest.h',
  'primitivevaluetype.h'
]

source_files = [
  'primitivevaluetype.c'
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
