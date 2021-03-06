# install.packages("plotly")

library(igraph)

# -----------------------------------
# get files
path <- ''
year <- '2022'

nodes <- read.csv(paste(path, 'nodes', year, '.csv', sep = ''), stringsAsFactors=F)
edges <- read.csv(paste(path, 'edges', year, '.csv', sep = ''), stringsAsFactors=F)
clr <- read.csv(paste(path, 'ColourMap.csv', sep = ''), stringsAsFactors=F)
shp <- read.csv(paste(path, 'ShapeMap.csv', sep = ''), stringsAsFactors=F)

# table(nodes$type)
# -----------------------------------
# map data
nodes <- merge(nodes, clr, by.x = "ObjectColour", by.y = "Shape.Colour", all.x=T )
nodes <- merge(nodes, clr, by.x = "Object.Base.Colour", by.y = "Shape.Colour", all.x=T)
nodes <- merge(nodes, shp, by.x = "ObjectType", by.y = "ShapeName" , all.x=T)

nodes$Hex.x[is.na(nodes$Hex.x)] <- 'AAAAAAAA'
nodes$Hex.y[is.na(nodes$Hex.y)] <- 'AAAAAAAA'
nodes$ShapeCode[is.na(nodes$ShapeCode)] <- 'CUB'

nodes <- nodes[ ,names(nodes[4:length(names(nodes))])]

nodes$nonMarkets[is.na(nodes$nonMarkets)] <- 0
names(nodes)[(length(names(nodes)) - 2):length(names(nodes))] <- c("ObjectColour", "ObjectBaseColour", "ObjectType")

# table(nodes$Object.Base.Colour)
# unique(nodes$Object.Base.Colour)
# cbind(unique(nodes$ObjectColour), clr$Shape.Colour)
# table(nodes$Hex.y)


# -----------------------------------
# constuct graph
g <- graph.data.frame(edges, directed = TRUE, vertices = nodes)

plot(g)

# -----------------------------------
# layout 2d
# -----------------------------------

# l <- layout_with_fr(g, dim = 2, niter = 500)
# l <- layout_with_kk(g, dim = 2, maxiter = 500)
# l <- layout_with_mds(g)
# l <- layout_with_sugiyama(g, layers = nodes$LayerOrdinal)
# l <- layout_with_dh(g)
l <- layout_with_graphopt(g, mass = 1, charge = 100)
# l <- layout_with_lgl(g, maxiter = 500)
# l <- norm_coords(cbind(l.fr, nodes$LayerOrdinal))
# l <- layout_on_grid(g)
# l <- layout_as_tree

# l <- layout_with_fr(g, dim = 3, niter = 500)

l <- l / max(l) * 10


# hist(l[, 1])

# l[, 3] <- nodes$LayerOrdinal

plot(g, layout = l, recale=F)

# g.1 = subgraph(g, V(g)[V(g)$LayerOrdinal == 1])
# plot(g.1)
# plot(g.1, layout = l[V(g)$LayerOrdinal == 1, ])
# l.1 <- layout_on_grid(g.1)
# plot(g.1, layout = l.1)
# 
# g.2 = subgraph(g, V(g)[V(g)$LayerOrdinal == 2])
# plot(g.2, layout = l[V(g)$LayerOrdinal == 2, ])
# l.2 <- layout_on_grid(g.2)
# plot(g.2, layout = l.2)
# 
# g.3 = subgraph(g, V(g)[V(g)$LayerOrdinal == 3])
# plot(g.3, layout = l[V(g)$LayerOrdinal == 3, ])
# 
# g.4 = subgraph(g, V(g)[V(g)$LayerOrdinal == 4])
# plot(g.4, layout = l[V(g)$LayerOrdinal == 4, ])


# l.df <- data.frame(l.fr)
# names(l.df)
# names(nodes)
# l.df$node = nodes$node
# l.df$type = nodes$type


nodes.layout <- cbind(nodes, l)

names(nodes.layout)[17:18] <- c("X", "Z")


# -----------------------------------
# centre the graph
nodes.layout$LayerOrdinal <- nodes.layout$LayerOrdinal - round(mean(nodes.layout$LayerOrdinal), 0)
nodes.layout$X <- nodes.layout$X - mean(nodes.layout$X)
nodes.layout$Z <- nodes.layout$Z - mean(nodes.layout$Z)

layer.x = ceiling(max(abs(nodes.layout$X)))
layer.z = ceiling(max(abs(nodes.layout$Z)))

# -----------------------------------
# set coords as vertex attribute 
V(g)$X <- nodes.layout$X
V(g)$Z <- nodes.layout$Z

# cbind(V(g)$name, V(g)$Y, V(g)$Z, nodes.layout$name, nodes.layout$Y, nodes.layout$Z)

# -----------------------------------
# get layers
layers <- unique(nodes[order(nodes$LayerOrdinal), c("LayerOrdinal", "Layer")])

layers$x = layer.x
layers$z = layer.z

# -----------------------------------
# write files
write_graph(g, "landscape.xml", format="graphml")
write.table(layers, "layers.csv", sep = ",", row.names = FALSE, quote=FALSE, col.names = FALSE)
