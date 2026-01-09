project('girtest', 'c', version: '0.1')

gobject_dep = dependency('gobject-2.0', version: '>= 2.66.0')

header_files = [
  'girtest.h',
  'girtest-alias-tester.h',
  'girtest-bitfield-tester.h',
  'girtest-byte-array-tester.h',
  'girtest-bytes-tester.h',
  'girtest-callback-tester.h',
  'girtest-class-tester.h',
  'girtest-clong-record-tester.h',
  'girtest-constant-tester.h',
  'girtest-culong-record-tester.h',
  'girtest-enum-tester.h',
  'girtest-error-tester.h',
  'girtest-initially-unowned-instance-creation-tester.h',
  'girtest-instance-creation-tester.h',
  'girtest-integer-array-tester.h',
  'girtest-interface-signal-tester.h',
  'girtest-long-record-tester.h',
  'girtest-method-hiding.h',
  'girtest-opaque-typed-record-copy-annotation-tester.h',
  'girtest-opaque-typed-record-tester.h',
  'girtest-opaque-untyped-record-tester.h',
  'girtest-platform-string-array-null-terminated-tester.h',
  'girtest-platform-string-array-sized-tester.h',
  'girtest-primitive-value-type-tester.h',
  'girtest-property-tester.h',
  'girtest-rename-to-tester.h',
  'girtest-returning-signal-tester.h',
  'girtest-signal-tester.h',
  'girtest-string-array-tester.h',
  'girtest-string-tester.h',
  'girtest-typed-record-copy-annotation-tester.h',
  'girtest-typed-record-tester.h',
  'girtest-ulong-record-tester.h',
  'girtest-untyped-record-tester.h',
  'girtest-utf8-string-array-null-terminated-tester.h',
  'girtest-utf8-string-array-sized-tester.h',
  'data/girtest-executor.h',
  'data/girtest-executor-impl.h',
  'data/girtest-signaler.h'
]

header_files_private = [
  'data/girtest-executor-private-impl.h'
] 

source_files = [
  'girtest-alias-tester.c',
  'girtest-bitfield-tester.c',
  'girtest-byte-array-tester.c',
  'girtest-bytes-tester.c',
  'girtest-callback-tester.c',
  'girtest-class-tester.c',
  'girtest-clong-record-tester.c',
  'girtest-culong-record-tester.c',
  'girtest-enum-tester.c',
  'girtest-error-tester.c',
  'girtest-initially-unowned-instance-creation-tester.c',
  'girtest-instance-creation-tester.c',
  'girtest-integer-array-tester.c',
  'girtest-interface-signal-tester.c',
  'girtest-long-record-tester.c',
  'girtest-method-hiding.c',
  'girtest-opaque-typed-record-copy-annotation-tester.c',
  'girtest-opaque-typed-record-tester.c',
  'girtest-opaque-untyped-record-tester.c',
  'girtest-platform-string-array-null-terminated-tester.c',
  'girtest-platform-string-array-sized-tester.c',
  'girtest-primitive-value-type-tester.c',
  'girtest-property-tester.c',
  'girtest-rename-to-tester.c',
  'girtest-returning-signal-tester.c',
  'girtest-signal-tester.c',
  'girtest-string-array-tester.c',
  'girtest-string-tester.c',
  'girtest-typed-record-copy-annotation-tester.c',
  'girtest-typed-record-tester.c',
  'girtest-ulong-record-tester.c',
  'girtest-untyped-record-tester.c',
  'girtest-utf8-string-array-null-terminated-tester.c',
  'girtest-utf8-string-array-sized-tester.c',
  'data/girtest-executor.c',
  'data/girtest-executor-impl.c',
  'data/girtest-signaler.c'
]

soruce_files_private = [
  'data/girtest-executor-private-impl.c'
]

# Build a shared library that depends on gobject.
lib = library('girtest',
  source_files + soruce_files_private,
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
  fatal_warnings: false, #TODO Enable if https://gitlab.gnome.org/GNOME/gobject-introspection/-/merge_requests/424 is merged
)
